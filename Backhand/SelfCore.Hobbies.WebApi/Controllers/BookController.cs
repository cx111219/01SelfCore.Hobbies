using SelfCore.Hobbies.Domains;
using SelfCore.Hobbies.Domains.Models;
using SelfCore.Hobbies.Services;
using SelfCore.Hobbies.Services.Helpers;
using System;
using System.Linq;

namespace SelfCore.Hobbies.WebApi.Controllers
{
    public class BookController : BaseApiController<Book>
    {
        public BookController(HobbyContext context) : base(context)
        {
        }
        /// <summary>
        /// 条件检索
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        protected override void QueryBefore(ref IQueryable<Book> query, QueryParam param)
        {
            if (!string.IsNullOrWhiteSpace(param.Keyword))
                query = query.Where(t => t.Author.Contains(param.Keyword) || t.Name.Contains(param.Keyword)) ;

            if (param.Type>0)
                query = query.Where(t => t.BookType == param.Type.Value);
        }

        protected override void AddBeforAsync(Book entity)
        {
            var user = _context.Books.FirstOrDefault(t => t.Name == entity.Name&& t.Author == entity.Author && !t.IsDeleted);
            if (user != null)
                throw new Exception($"已存在名为 {entity.Name} 的书籍！");
            
            entity.Creatime = DateTime.Now;
            entity.Creator = Web.UserId;
        }

        protected override void UpdateBeforeAsync(Book entity)
        {
            var user = _context.Books.FirstOrDefault(t => t.Name == entity.Name && t.Author == entity.Author && t.Id != entity.Id && t.IsDeleted);
            if (user != null)
                throw new Exception($"已存在名为 {entity.Name} 的书籍！");
        }
    }
}
