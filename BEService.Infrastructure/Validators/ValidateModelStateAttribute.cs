using BEService.Core.CustomEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BEService.Infrastructure.Validators
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                var response = new ApiResponse<List<string>>(errors)
                {
                    Message = new Message
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.UnprocessableEntity,
                        Title = HttpStatusCode.UnprocessableEntity.ToString(),
                        Description = errors[0]
                    }
                };


                context.Result = new JsonResult(response)
                {
                    StatusCode = (int)HttpStatusCode.UnprocessableEntity
                };
            }
        }
    }
}
