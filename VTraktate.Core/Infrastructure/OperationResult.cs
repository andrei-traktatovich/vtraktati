using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;

namespace VTraktate.Core.Infrastructure
{
    public class OperationResult
    {
        public const int UNAUTHORIZED = 401;
        public const int ENTITY_NOT_FOUND = 404;

        
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; private set; }
        public bool Success { get; private set; }
        public string ErrorMessage { get; private set; }
        public int StatusCode { get; private set; }

        internal OperationResult(bool success, T data = default(T), string errorMessage = null, int statusCode = 0)
        {
            Success = success;
            Data = data;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public static OperationResult<T> Ok(T data)
        {
            return new OperationResult<T>(true, data);
        }

        public static OperationResult<T> Error(string errorMessage, int statusCode = 0)
        {
            return new OperationResult<T>(false, default(T), errorMessage, statusCode);
        }

        public static OperationResult<T> Unauthorized(string errorMessage)
        {
            return new OperationResult<T>(false, default(T), errorMessage, UNAUTHORIZED);
        }

        public static OperationResult<T> NotFound(string errorMessage)
        {
            return new OperationResult<T>(false, default(T), errorMessage, ENTITY_NOT_FOUND);
        }
    }
}