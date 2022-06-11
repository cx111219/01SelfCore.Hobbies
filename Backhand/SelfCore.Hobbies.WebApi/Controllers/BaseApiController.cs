using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfCore.Hobbies.Domains;
using SelfCore.Hobbies.Services.Interceptors;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfCore.Hobbies.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionHandler]
    [TraceLog]
    public class BaseApiController<Entity> : QueryController<Entity> where Entity :class, IDeleted, Ikey, new()
    {
        public BaseApiController(HobbyContext context) : base(context)
        {
        }

        /// <summary>
        /// 新增实例
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public virtual async Task<IActionResult> AddAsync([FromBody] Entity entity) {
            if (entity == null)
                return Fail("参数异常！");
            AddBeforAsync(entity);
            await _context.Set<Entity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return Success(entity);
        }

        /// <summary>
        /// 添加数据校验
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void AddBeforAsync(Entity entity) {
        }

        /// <summary>
        /// 更改数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateAsync(int id , [FromBody] Entity entity) {
            if (id == 0)
                return Fail("参数异常！");
            var model = await _context.FindAsync<Entity>(id);
            if (model == null)
                return Fail("为找到该实体对象！");
            UpdateBeforeAsync(model, entity);
            _context.Entry(model).State = EntityState.Modified; //  防止多次更改？？
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return Success(entity);
        }

        /// <summary>
        /// update 前数据校验
        /// </summary>
        /// <param name="oldEntity"></param>
        /// <param name="entity"></param>
        protected virtual void UpdateBeforeAsync(Entity oldEntity, Entity entity) { }

        /// <summary>
        /// 单个实体删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            if (id == 0)
                return Fail("参数异常！");
            var model = await _context.FindAsync<Entity>(id);
            if (model == null)
                return Fail("未找到该实体对象！");
            DeleteBefore(model);
            if (model is IDeleted)
            {
                model.IsDeleted = true;
                model.Creatime = System.DateTime.Now;
                // model.Creator = User.Id;
                _context.Update(model);
            }
            else
                _context.Remove(model);
            await _context.SaveChangesAsync();
            return Success(null);
        }

        /// <summary>
        /// 单个删除前操作
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void DeleteBefore(Entity entity) { }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete("delBatch/{ids}")]
        public async Task<IActionResult> DeleteBatchAsync(string ids) {
            if (string.IsNullOrWhiteSpace(ids))
                return Fail("参数异常！");
            List<int> idList = ids.Split(",").Select(t=>int.Parse(t)).ToList();
            var entities =_context.Set<Entity>().Where(t=>idList.Contains(t.Id)).ToList();
            if(entities==null || !entities.Any())
                return Fail("参数异常！");
            DeleteBatchBefore(entities);
            if (entities[0] is IDeleted) {
                entities.ForEach(t =>
                {
                    t.IsDeleted = true;
                    t.Creatime = System.DateTime.Now;
                    // t.Creator = User.Id;
                });
                _context.UpdateRange(entities);
            }
            else
                _context.RemoveRange(entities);            
            await _context.SaveChangesAsync();
            return Success(null);
        }

        protected virtual void DeleteBatchBefore(List<Entity> entities) { }       
    }

}
