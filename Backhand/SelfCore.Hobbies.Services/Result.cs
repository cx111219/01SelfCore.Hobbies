using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SelfCore.Hobbies.Services
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class Result : JsonResult
    {
        public  ResultCode Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public Result(ResultCode code,string message,object data=null):base(null) {
            Code = code;
            Message = message;
            Data = data;
        }
        public Result(ResultCode code, object data = null)
            : this(code,"操作成功!", data){
        }
        public override Task ExecuteResultAsync(ActionContext context)
        {
            this.Value = new { Code, Message, Data };
            return base.ExecuteResultAsync(context);
        }
        public override void ExecuteResult(ActionContext context)
        {
            this.Value = new { Code, Message, Data };
            base.ExecuteResult(context);
        }
    }

    public enum ResultCode { 
        Success,
        Fail
    }
}
