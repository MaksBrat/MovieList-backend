using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieList.Domain.ApiError;
using MovieList.Services.Exceptions;
using MovieList.Services.Exceptions.Base;
using System.Net;

namespace MovieList.Attributes
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<ApiExceptionFilterAttribute>>();

            switch (context.Exception)
            {
                case RecordNotFoundException recordNotFoundException:
                {
                    var initialRequestMethod = context?.HttpContext?.Request?.Method;

                    var apiError = new ApiError();

                    if (HttpMethods.IsGet(initialRequestMethod))
                    {
                        apiError.Message = recordNotFoundException.Message;
                        apiError.ErrorId = recordNotFoundException.ErrorId;
                        apiError.HttpStatusCode = (int)HttpStatusCode.NotFound;
                    }
                    else
                    {
                        apiError.Message = recordNotFoundException.Message;
                        apiError.ErrorId = ErrorIdConstans.UnprocessableEntity;
                        apiError.HttpStatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    }

                    logger.LogError(recordNotFoundException, apiError.Message);

                    SetResponse(context, apiError);
                    break;
                }

                case CustomizedResponseException customizedResponseException:
                {
                    var apiError = new ApiError
                    {
                        Message = customizedResponseException.Message,
                        ErrorId = customizedResponseException.ErrorId,
                        HttpStatusCode = customizedResponseException.HttpStatusCode
                    };

                    logger.LogError(customizedResponseException, apiError.Message);

                    SetResponse(context, apiError);
                    break;
                }

                case MovieListBaseException baseException:
                {
                    var apiError = new ApiError
                    {
                        Message = baseException.Message,
                        ErrorId = baseException.ErrorId,
                        HttpStatusCode = (int)HttpStatusCode.BadRequest
                    };

                    logger.LogError(baseException, apiError.Message);

                    SetResponse(context, apiError);
                    break;
                }

                default:
                {
                    var apiError = new ApiError
                    {
                        Message = "MovieList Unexpected exception.",
                        ErrorId = ErrorIdConstans.UnexpectedError,
                        HttpStatusCode = (int)HttpStatusCode.InternalServerError
                    };

                    logger.LogError(context.Exception, apiError.Message);

                    SetResponse(context, apiError);
                    break;
                }
            }           
        }

        private void SetResponse(ExceptionContext context, ApiError apiError)
        {
            context.Result = new JsonResult(apiError);
            context.HttpContext.Response.StatusCode = apiError.HttpStatusCode ?? (int)HttpStatusCode.BadRequest;
        }
    }
}
