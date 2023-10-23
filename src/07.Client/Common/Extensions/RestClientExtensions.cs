﻿using Pertamina.Website_KPI.Client.Services.UserInfo;
using Pertamina.Website_KPI.Shared.Common.Constants;
using RestSharp;

namespace Pertamina.Website_KPI.Client.Common.Extensions;
public static class RestClientExtensions
{
    private const string Bearer = nameof(Bearer);

    public static void AddUserInfo(this RestClient restClient, UserInfoService userInfoService)
    {
        restClient.AddDefaultHeader(HttpHeaderName.Authorization, $"{Bearer} {userInfoService.AccessToken}");

        if (!string.IsNullOrWhiteSpace(userInfoService.PositionId))
        {
            restClient.AddDefaultHeader(HttpHeaderName.PtmnPositionId, userInfoService.PositionId);
        }

        if (!string.IsNullOrWhiteSpace(userInfoService.IpAddress))
        {
            restClient.AddDefaultHeader(HttpHeaderName.PtmnIpAddress, userInfoService.IpAddress);
        }

        if (userInfoService.Geolocation is not null)
        {
            restClient.AddDefaultHeader(HttpHeaderName.PtmnGeolocation, userInfoService.Geolocation.ToString());
        }
    }
}
