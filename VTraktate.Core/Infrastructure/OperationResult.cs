using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Core.Infrastructure
{
    public class OperationResult<T>
    {
        public T Data { get; private set; }
        public bool Success { get; private set; }
        public string ErrorMessage { get; private set; }

        private OperationResult()
        {
        }

        public static OperationResult<T> Ok(T data)
        {
            return new OperationResult<T>
            {
                Data = data,
                Success = true,
                ErrorMessage = null
            };
        }

        public static OperationResult<T> Error(string errorMessage)
        {
            return new OperationResult<T>
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }
}