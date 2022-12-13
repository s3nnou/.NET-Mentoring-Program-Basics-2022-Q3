using BrainstormSessions.Api;
using log4net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BrainstormSessions.Filters
{
    public class LogActionAttribute : ActionFilterAttribute
    {
        private readonly ILogger<LogActionAttribute> _log;

        public LogActionAttribute(ILogger<LogActionAttribute> log)
        {
            _log = log;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = ((Microsoft.AspNetCore.Mvc.ControllerBase)filterContext.Controller)
                                           .ControllerContext
                                           .ActionDescriptor
                                           .ControllerName;
            var actionName = ((Microsoft.AspNetCore.Mvc.ControllerBase)filterContext.Controller)
                                    .ControllerContext
                                    .ActionDescriptor
                                    .ActionName;

            _log.LogInformation($"Get into {actionName} action in {controllerName} controller");
        }
    }
}
