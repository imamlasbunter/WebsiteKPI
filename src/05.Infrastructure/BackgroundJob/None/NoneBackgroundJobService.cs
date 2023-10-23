﻿using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Pertamina.Website_KPI.Application.Services.BackgroundJob;
using Pertamina.Website_KPI.Shared.Common.Constants;
using Pertamina.Website_KPI.Shared.Common.Extensions;

namespace Pertamina.Website_KPI.Infrastructure.BackgroundJob.None;
public class NoneBackgroundJobService : IBackgroundJobService
{
    private readonly ILogger<NoneBackgroundJobService> _logger;

    public NoneBackgroundJobService(ILogger<NoneBackgroundJobService> logger)
    {
        _logger = logger;
    }

    private void LogWarning()
    {
        _logger.LogWarning("{ServiceName} is set to None.", $"{nameof(BackgroundJob).SplitWords()} {CommonDisplayTextFor.Service}");
    }

    public Task<bool> AddContinuationJob(string parentJobId, Expression<Func<Task>> methodCall)
    {
        LogWarning();

        return Task.FromResult(false);
    }

    public Task<bool> AddRecurringJob(string recurringJobId, Expression<Func<Task>> methodCall, string cronExpression, TimeZoneInfo timeZone)
    {
        LogWarning();

        return Task.FromResult(false);
    }

    public Task<string> AddScheduledJob(Expression<Func<Task>> methodCall, DateTimeOffset enqueueAt)
    {
        LogWarning();

        return Task.FromResult(string.Empty);
    }

    public Task<bool> RemoveRecurringJob(string recurringJobId)
    {
        LogWarning();

        return Task.FromResult(false);
    }

    public Task<bool> RemoveScheduledJob(string scheduledJobId)
    {
        LogWarning();

        return Task.FromResult(false);
    }

    public Task<string> RunJob(Expression<Func<Task>> methodCall)
    {
        LogWarning();

        return Task.FromResult(string.Empty);
    }

    public Task<bool> UpdateRecurringJob(string recurringJobId, Expression<Func<Task>> methodCall, string cronExpression, TimeZoneInfo timeZone)
    {
        LogWarning();

        return Task.FromResult(false);
    }
}
