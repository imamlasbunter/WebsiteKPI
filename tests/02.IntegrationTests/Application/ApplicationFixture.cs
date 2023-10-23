using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pertamina.Website_KPI.Application;
using Pertamina.Website_KPI.Application.Common.Exceptions;
using Pertamina.Website_KPI.Application.Services.Authorization;
using Pertamina.Website_KPI.Application.Services.CurrentUser;
using Pertamina.Website_KPI.Application.Services.UserProfile;
using Pertamina.Website_KPI.Infrastructure;
using Pertamina.Website_KPI.Infrastructure.Logging;
using Pertamina.Website_KPI.Infrastructure.Persistence.Common.Constants;
using Pertamina.Website_KPI.Infrastructure.Persistence.SqlServer;
using Pertamina.Website_KPI.IntegrationTests.Infrastructure.Authorization;
using Pertamina.Website_KPI.IntegrationTests.Infrastructure.CurrentUser;
using Pertamina.Website_KPI.IntegrationTests.Infrastructure.UserProfile;
using Pertamina.Website_KPI.IntegrationTests.Repositories.Users;
using Pertamina.Website_KPI.IntegrationTests.Repositories.Users.Models;
using Pertamina.Website_KPI.Shared;
using Pertamina.Website_KPI.Shared.Common.Constants;
using Respawn;
using Respawn.Graph;

namespace Pertamina.Website_KPI.IntegrationTests.Application;
public class ApplicationFixture : IDisposable
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfigurationRoot _configuration;
    private readonly Checkpoint _checkpoint;

    public FakeCurrentUserService CurrentUser { get; private set; }

    public ApplicationFixture()
    {
        SetupEnvironmentVariables();

        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"{AppContext.BaseDirectory}appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{AppContext.BaseDirectory}appsettings.{CommonValueFor.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        _configuration = configurationBuilder.Build();

        var services = new ServiceCollection()
            .AddLogging(logging => logging.AddSimpleConsole(LoggingHelper.SimpleConsoleOptions));

        services.AddShared(_configuration);
        services.AddApplication();
        services.AddInfrastructure(_configuration);

        services.AddSingleton(Mock.Of<IWebHostEnvironment>(webHostEnvironment =>
            webHostEnvironment.EnvironmentName == CommonValueFor.EnvironmentName &&
            webHostEnvironment.ApplicationName == typeof(ApplicationFixture).Assembly.FullName));

        var currentUserServiceDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(ICurrentUserService))!;
        services.Remove(currentUserServiceDescriptor);

        CurrentUser = new FakeCurrentUserService();
        services.AddTransient(provider => Mock.Of<ICurrentUserService>(currentUser =>
            currentUser.UserId == CurrentUser.UserId &&
            currentUser.Username == CurrentUser.Username &&
            currentUser.PositionId == CurrentUser.PositionId &&
            currentUser.ClientId == CurrentUser.ClientId &&
            currentUser.IpAddress == CurrentUser.IpAddress &&
            currentUser.Geolocation == CurrentUser.Geolocation));

        services.AddTransient<IAuthorizationService, FileBasedAuthorizationService>();
        services.AddTransient<IUserProfileService, FileBasedUserProfileService>();

        _scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

        _checkpoint = new Checkpoint
        {
            TablesToIgnore = new[] { new Table(TableNameFor.EfMigrationsHistory) }
        };

        EnsureDatabase();
    }

    private static void SetupEnvironmentVariables()
    {
        using var file = File.OpenText("Properties\\launchSettings.json");
        var reader = new JsonTextReader(file);
        var jObject = JObject.Load(reader);

        var variables = jObject
            .GetValue("profiles")!
            .SelectMany(profiles => profiles.Children())
            .SelectMany(profile => profile.Children<JProperty>())
            .Where(prop => prop.Name == "environmentVariables")
            .SelectMany(prop => prop.Value.Children<JProperty>())
            .ToList();

        foreach (var variable in variables)
        {
            Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
        }
    }

    private void EnsureDatabase()
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<SqlServerWebsite_KPIDbContext>();

        context.Database.Migrate();
    }

    public void RunAsUser(string username, string? positionId = null)
    {
        var user = UserRepository.Users.Where(x => x.Username == username).SingleOrDefault();

        if (user is null)
        {
            throw new NotFoundException(nameof(User), nameof(User.Username), username);
        }

        CurrentUser = new FakeCurrentUserService(user.Id, user.Username, positionId);
    }

    public void RunAsUnauthenticatedUser()
    {
        CurrentUser = new FakeCurrentUserService();
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<SqlServerWebsite_KPIDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public async Task ResetState()
    {
        var sqlServerOptions = _configuration.GetSection(SqlServerOptions.SectionKey).Get<SqlServerOptions>();

        await _checkpoint.Reset(sqlServerOptions.ConnectionString);

        CurrentUser = new FakeCurrentUserService();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
