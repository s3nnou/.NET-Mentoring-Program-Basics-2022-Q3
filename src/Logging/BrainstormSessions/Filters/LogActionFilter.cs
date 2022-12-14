using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BrainstormSessions.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger<LogActionFilter> _log;

        public LogActionFilter(ILogger<LogActionFilter> log)
        {
            _log = log;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controllerName = ((Microsoft.AspNetCore.Mvc.ControllerBase)context.Controller)
                               .ControllerContext
                               .ActionDescriptor
                               .ControllerName;
            var actionName = ((Microsoft.AspNetCore.Mvc.ControllerBase)context.Controller)
                                    .ControllerContext
                                    .ActionDescriptor
                                    .ActionName;

            _log.LogInformation($"Leaving {actionName} action in {controllerName} controller");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = ((Microsoft.AspNetCore.Mvc.ControllerBase)context.Controller)
                               .ControllerContext
                               .ActionDescriptor
                               .ControllerName;
            var actionName = ((Microsoft.AspNetCore.Mvc.ControllerBase)context.Controller)
                                    .ControllerContext
                                    .ActionDescriptor
                                    .ActionName;

            _log.LogInformation($"Getting into {actionName} action in {controllerName} controller");
        }
    }
}