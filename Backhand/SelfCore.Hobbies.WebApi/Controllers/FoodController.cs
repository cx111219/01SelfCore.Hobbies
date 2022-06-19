using SelfCore.Hobbies.Domains;
using SelfCore.Hobbies.Domains.Models;
using SelfCore.Hobbies.Services;
using System.Linq;

namespace SelfCore.Hobbies.WebApi.Controllers
{
    public class FoodController : BaseApiController<Food>
    {
        public FoodController(HobbyContext context) : base(context)
        {
        }
        /// <summary>
        /// 条件检索
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        protected override void QueryBefore(ref IQueryable<Food> query, QueryParam param)
        {
            if (!string.IsNullOrWhiteSpace(param.Keyword))
                query = query.Where(t => t.Remark.Contains(param.Keyword));
        }
    }
}
