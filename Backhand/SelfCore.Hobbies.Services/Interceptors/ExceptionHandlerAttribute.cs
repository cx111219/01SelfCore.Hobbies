using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System;

namespace SelfCore.Hobbies.Services.Interceptors
{
    /// <summary>
    /// 异常拦截器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionHandlerAttribute: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            Logger logger = NLog.LogManager.GetLogger("ExcptionLog");
            logger.Log(LogLevel.Error, context.Exception.Message);
            logger.Log(LogLevel.Info, context.Exception.StackTrace);

            context.Result = new Result(ResultCode.Fail, context.Exception.Message);
            context.HttpContext.Response.StatusCode = 200;
            context.ExceptionHandled = true;
        }
    }
}
