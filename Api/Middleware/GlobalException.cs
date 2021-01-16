using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Enums;
using System.Reflection;
using Application.Exceptions;

namespace Api.Middleware
{
    public class GlobalException
    {
        private readonly RequestDelegate _next;

        public GlobalException(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;
            Exception exception = null;

            try
            {
                context.Response.Body = new MemoryStream();
                await _next(context);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                using (var newBody = new MemoryStream())
                {
                    var oldBody = context.Response.Body;
                    string content;

                    context.Response.Body = newBody;
                    oldBody.Seek(0, SeekOrigin.Begin);

                    var reader = new StreamReader(oldBody);

                    content = await reader.ReadToEndAsync();

                    if (exception != null)
                    {
                        content = ErrorHandler(context.Response, exception);
                    }
                    else if (context.Response.StatusCode >= StatusCodes.Status400BadRequest)
                    {
                        content = ErrorHandler(context.Response, content);
                    }

                    context.Response.Body = originalBody;
                    await context.Response.WriteAsync(content);
                    oldBody.Dispose();
                }
            }
        }


        private string ErrorHandler(HttpResponse response, Exception exception)
        {
            dynamic result;

            response.ContentType = "application/json";

            if (exception is ApiException)
            {
                var apiError = (ApiException)exception;

                response.StatusCode = apiError.StatusCode;
                result = new
                {
                    error = new
                    {
                        trace_id = response.HttpContext.TraceIdentifier,
                        code = (int)apiError.ErrorCode,
                        type = apiError.ExceptionType,
                        message = apiError.Message
                    }
                };
            }
            else
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                result = new
                {
                    error = new
                    {
                        trace_id = response.HttpContext.TraceIdentifier,
                        code = (int)ErrorCodes.GeneralException,
                        type = exception.GetType().ToString(),
                        message = exception.Message
                    }
                };
            }

            return JsonConvert.SerializeObject(result);
        }

        private string ErrorHandler(HttpResponse response, string content)
        {
            var errMsg = string.Empty;

            if (!string.IsNullOrEmpty(content))
            {
                var schema = new
                {
                    error = new
                    {
                        trace_id = string.Empty,
                        code = int.MinValue,
                        type = string.Empty,
                        message = string.Empty
                    }
                };
                var contentObj = JsonConvert.DeserializeAnonymousType(content, schema);

                if (contentObj != null) return content;
                else errMsg = content;
            }

            response.ContentType = "application/json";

            ErrorCodes errCode;
            switch (response.StatusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    errCode = ErrorCodes.NotAuthorized;
                    break;
                default:
                    errCode = ErrorCodes.GeneralException;
                    break;
            }

            if (string.IsNullOrEmpty(errMsg))
            {
                errMsg = errCode
                            .GetType()
                            .GetMember(errCode.ToString())
                            .FirstOrDefault()
                            ?.GetCustomAttribute<DescriptionAttribute>()
                            ?.Description;
            }

            return JsonConvert.SerializeObject(new
            {
                error = new
                {
                    trace_id = response.HttpContext.TraceIdentifier,
                    code = (int)errCode,
                    type = Enum.GetName(typeof(ErrorCodes), errCode),
                    message = errMsg
                }
            });
        }
    }
}
