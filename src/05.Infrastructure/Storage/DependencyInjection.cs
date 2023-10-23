using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Infrastructure.Storage.AzureBlob;
using Pertamina.Website_KPI.Infrastructure.Storage.LocalFolder;
using Pertamina.Website_KPI.Infrastructure.Storage.None;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Infrastructure.Storage;
public static class DependencyInjection
{
    public static IServiceCollection AddStorageService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        var storageOptions = configuration.GetSection(StorageOptions.SectionKey).Get<StorageOptions>();

        switch (storageOptions.Provider)
        {
            case StorageProvider.None:
                services.AddNoneStorageService();
                break;
            case StorageProvider.LocalFolder:
                services.AddLocalFolderStorageService(configuration, healthChecksBuilder);
                break;
            case StorageProvider.AzureBlob:
                services.AddAzureBlobStorageService(configuration, healthChecksBuilder);
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Storage)} {nameof(StorageOptions.Provider)}: {storageOptions.Provider}");
        }

        return services;
    }
}
