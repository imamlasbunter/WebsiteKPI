﻿using Pertamina.Website_KPI.Shared.Services.HealthCheck.Constants;
using Pertamina.Website_KPI.Shared.Services.HealthCheck.Queries.GetHealthCheck;
using RestSharp;

namespace Pertamina.Website_KPI.Client.Services.HealthCheck;
public class HealthCheckService
{
    private readonly RestClient _restClient;

    public HealthCheckService()
    {
        _restClient = new RestClient();
    }

    public async Task<GetHealthCheckResponse> GetHealthCheckAsync(string healthCheckUrl)
    {
        var restRequest = new RestRequest(healthCheckUrl, Method.Get);
        var restResponse = await _restClient.ExecuteAsync<GetHealthCheckResponse>(restRequest);

        if (!restResponse.IsSuccessful)
        {
            return new GetHealthCheckResponse
            {
                Status = HealthCheckStatus.Unhealthy
            };
        }

        if (restResponse.Data is null)
        {
            return new GetHealthCheckResponse
            {
                Status = HealthCheckStatus.Unknown
            };
        }

        return restResponse.Data;
    }
}
