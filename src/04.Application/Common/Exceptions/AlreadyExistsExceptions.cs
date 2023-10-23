﻿using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Application.Common.Exceptions;
public class AlreadyExistsExceptions : Exception
{
    public AlreadyExistsExceptions()
        : base()
    {
    }

    public AlreadyExistsExceptions(string message)
        : base(message)
    {
    }

    public AlreadyExistsExceptions(string entityName, object key)
        : base($"{entityName} with {CommonDisplayTextFor.Id} [{key}] already exists.")
    {
    }

    public AlreadyExistsExceptions(string entityName, string entityField, object value)
        : base($"{entityName} with {entityField}: {value} already exists.")
    {
    }
}
