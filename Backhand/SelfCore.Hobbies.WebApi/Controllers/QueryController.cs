using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfCore.Hobbies.Domains;
using SelfCore.Hobbies.Services;
using SelfCore.Hobbies.Services.Parameters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfCore.Hobbies.WebApi.Controllers
{
    public class QueryController<Entity> : ControllerBase where Entity : class,Ikey
    {
        protected readonly HobbyContext _context;
        public QueryController(HobbyContext context) {
            _context = context;
        }
        /// <summary>
        /// 获取单个实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("single")]
        public virtual async Task<IActionResult> GetAsync(int id)
        {
            if (id == 0)
                return Fail("参数异常！");
            var data = await _context.Set<Entity>().FindAsync(id);
            return data == null ? Fail("未找到该实体！") : Success(data);
        }

        /// <summary>
        /// 获取所有列表数据 不分页
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public virtual async Task<IActionResult> GetListAsync()
        {
            var data = await _context.Set<Entity>()
                .OrderBy(t => t.Id)
                 .ToListAsync();
            return Success(data);
        }

        /// <summary>
        /// 数据 分页 PagerLis
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet("pagerList")]
        public async Task<IActionResult> GetPagerListAsync([FromQuery] QueryParam param)
        {
            var query = _context.Set<Entity>().AsQueryable();
            QueryBefore(query, param);
            var data = await QueryToPagerListAsync(query,param);
            return Success(data);
        }
        
        /// <summary>
        /// 检索操作
        /// </summary>
        /// <param name="query"></param>
        /// <param name="keyword"></param>
        protected virtual void QueryBefore(IQueryable<Entity> query, QueryParam keyword)
        {
        }
       
        /// <summary>
        /// 获取列表 分页
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<PagerList<Entity>> QueryToPagerListAsync( IQueryable<Entity> query, QueryParam param)
        {
            int count = query.Count();
            var data = await query.Skip(param.Size.Value * (param.Page.Value - 1))
                 .Take(param.Size.Value)
                 .OrderBy(t=>t.Id)
                 .ToListAsync();
            return new PagerList<Entity>(count, param, data);
            // return Success(new PagerList<Entity>(count,param,data));
        }

        /// <summary>
        /// 获取列表 返回pagesize的数据 预留
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<IActionResult> QueryToListAsync(IQueryable<Entity> query, QueryParam param)
        {
            // int count = query.Count();
            var data = await query.Skip(param.Size.Value * (param.Page.Value - 1))
                 .Take(param.Size.Value)
                 .ToListAsync();
            return Success(data);
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected IActionResult Success(object data) => new Result(ResultCode.Success, data);

        protected IActionResult Fail(string message) => new Result(ResultCode.Fail, message);
    }

}
