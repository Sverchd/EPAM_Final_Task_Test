using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Faculty.Utils;

namespace Faculty.Filters
{
    public class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string requestUrl = context.HttpContext.Request.Url.ToString();
            string exceptionStack = context.Exception.StackTrace;
            string exceptionMessage = context.Exception.Message;
            Logger.Log.Error($"Requested url: {requestUrl} exception thrown: \n {exceptionMessage} \n {exceptionStack}");
            context.ExceptionHandled = true;
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Error"},
                    { "action", "InternalError"},
                }
            );
        }
    }
}