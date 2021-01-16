using Domain.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Application.Exceptions
{
    public class ApiException : Exception
    {
        public ErrorCodes ErrorCode { get; } = ErrorCodes.GeneralException;
        public string ExceptionType => Enum.GetName(typeof(ErrorCodes), ErrorCode);

        public int StatusCode { get; } = StatusCodes.Status400BadRequest;

        public ApiException(ErrorCodes errorCode) : base(FormatMessage(errorCode))
        {
            ErrorCode = errorCode;
        }
        public ApiException(ErrorCodes errorCode, Exception innerException) : base(FormatMessage(errorCode), innerException)
        {
            ErrorCode = errorCode;
        }
        public ApiException(ErrorCodes errorCode, int statusCode) : base(FormatMessage(errorCode))
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
        public ApiException(ErrorCodes errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
        public ApiException(ErrorCodes errorCode, string message, int statusCode) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
        public ApiException(ErrorCodes errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
        public ApiException(ErrorCodes errorCode, string message, int statusCode, Exception innerexception) : base(message, innerexception)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }

        private static string FormatMessage(Enum code)
        {
            string n = code.GetType().GetMember(code.ToString()).FirstOrDefault()
                        ?.GetCustomAttribute<DescriptionAttribute>()
                        ?.Description
                        ??
                        ErrorCodes.GeneralException.GetType().GetMember(ErrorCodes.GeneralException.ToString()).FirstOrDefault()
                        ?.GetCustomAttribute<DescriptionAttribute>()
                        ?.Description;

            return n;
        }
    }
}
