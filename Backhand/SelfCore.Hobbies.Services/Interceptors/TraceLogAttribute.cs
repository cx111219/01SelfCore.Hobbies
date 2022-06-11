using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using NLog;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SelfCore.Hobbies.Services.Interceptors
{
    /// <summary>
    /// api-trace 过滤器 应用在class 和 method 中
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TraceLogAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 日志名
        /// </summary>
        private const string TraceLogName = "ApiTraceLog";

        /// <summary>
        /// 是否忽略,为true不记录日志
        /// </summary>
        public bool Ignore { get; set; }
        /// <summary>
        /// nlog 日志管理器
        /// </summary>
        private Logger logger { get; set; }

        private StringBuilder message;

        /// <summary>
        /// action 执行前操作
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            message = new StringBuilder();
            logger = NLog.LogManager.GetLogger(TraceLogName);

            await base.OnActionExecutionAsync(context, next);
            if (Ignore) return;
            message.AppendLine("**************************************************************************");
            logger.Log(LogLevel.Trace, message);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (Ignore) return;
            Microsoft.AspNetCore.Http.HttpRequest request = context.HttpContext.Request;
            if (request == null)
                return;
            // 初始日志
            message.AppendLine($"{request.Method} - {request.Host}{request.Path}{request.QueryString} - {request.Protocol} ");
            if (context.ActionArguments != null)
                message.AppendLine($"参数：{JsonConvert.SerializeObject(context.ActionArguments)}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            if (Ignore) return;
            // api执行后 日志中添加 response
            var response = context.HttpContext.Response;
            message.Append($"Response: Http状态码:{response.StatusCode}");
            if (context.Result is Result result)
            {
                message.AppendLine($"Message :  {result.Message}");
                message.AppendLine($"Data :  {(result.Data == null ? null : JsonConvert.SerializeObject(result.Data))}");
            }
        }
    }
}
