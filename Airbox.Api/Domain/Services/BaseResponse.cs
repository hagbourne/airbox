﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Domain.Services
{
    /// <summary>
    /// Abstract base class for all service response classes.
    /// </summary>
    public abstract class BaseResponse
    {
        public bool Success { get; protected set; }

        public string Message { get; protected set; }

        public BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
