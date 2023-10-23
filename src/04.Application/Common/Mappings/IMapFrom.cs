﻿using AutoMapper;

namespace Pertamina.Website_KPI.Application.Common.Mappings;
public interface IMapFrom<TSource, TDestination>
{
    void Mapping(Profile profile)
    {
        profile.CreateMap<TSource, TDestination>();
    }
}
