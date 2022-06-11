using Microsoft.EntityFrameworkCore.Diagnostics;
using NLog;
using System.Data.Common;
using System.Text;

namespace SelfCore.Hobbies.Services.Interceptors
{
    /// <summary>
    /// sql 拦截器
    /// </summary>
    public class SqlLogInterceptor : DbCommandInterceptor
    {

        private const string TraceLogName = "SqlTraceLog";
        private StringBuilder message;
        private Logger logger = NLog.LogManager.GetLogger(TraceLogName);

        public override InterceptionResult DataReaderDisposing(DbCommand command, DataReaderDisposingEventData eventData, InterceptionResult result)
        {
            message = new StringBuilder();
            message.AppendLine($"DataReaderDisposing: {command.CommandText}");

            message.AppendLine("____________________________");
            logger.Info(message);
            return base.DataReaderDisposing(command, eventData, result);
        }
    }
}
