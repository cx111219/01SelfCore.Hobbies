using SelfCore.Hobbies.Domains;
using SelfCore.Hobbies.Domains.Models;
using SelfCore.Hobbies.Services;
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
        protected override void QueryBefore(IQueryable<Book> query, QueryParam param)
        {
            if (!string.IsNullOrWhiteSpace(param.Keyword))
            {
                query = query.Where(t => t.Author.Contains(param.Keyword) || t.Name.Contains(param.Keyword));
            }
        }
    }
}
