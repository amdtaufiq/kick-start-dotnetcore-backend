using BEService.Core.CustomEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BEService.Core.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception.GetType() == typeof(OkException))
            {
                var exception = (OkException)context.Exception;
                var response = new ApiResponse<string>(null);

                response.Message = new Message
                {
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.OK,
                    Title = HttpStatusCode.OK.ToString(),
                    Description = exception.Message
                };

                context.Result = new OkObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.ExceptionHandled = true;
            }

            if (context.Exception.GetType() == typeof(BadRequestException))
            {
                var exception = (BadRequestException)context.Exception;
                var response = new ApiResponse<string>(null);

                response.Message = new Message
                {
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Title = HttpStatusCode.BadRequest.ToString(),
                    Description = exception.Message
                };

                context.Result = new OkObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }

            if (context.Exception.GetType() == typeof(NotFoundException))
            {
                var exception = (NotFoundException)context.Exception;
                var response = new ApiResponse<string>(null);

                response.Message = new Message
                {
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Title = HttpStatusCode.NotFound.ToString(),
                    Description = exception.Message
                };

                context.Result = new OkObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.ExceptionHandled = true;
            }

            if (context.Exception.GetType() == typeof(UnprocessableEntityException))
            {
                var exception = (UnprocessableEntityException)context.Exception;
                var response = new ApiResponse<string>(null);

                response.Message = new Message
                {
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.UnprocessableEntity,
                    Title = HttpStatusCode.UnprocessableEntity.ToString(),
                    Description = exception.Message
                };

                context.Result = new OkObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                context.ExceptionHandled = true;
            }

            if (context.Exception.GetType() == typeof(InternalServerErrorException))
            {
                var exception = (InternalServerErrorException)context.Exception;
                var response = new ApiResponse<string>(null);

                response.Message = new Message
                {
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Title = HttpStatusCode.InternalServerError.ToString(),
                    Description = exception.Message
                };

                context.Result = new OkObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;
            }
        }
    }
}
