using System.Collections.Generic;

namespace SelfCore.Hobbies.Services.Parameters
{
    /// <summary>
    /// 分页器
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public class PagerList<Entity> where Entity : class
    {
        /// <summary>
        /// 当前页数，即第几页，从1开始
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 每页显示行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }

        public List<Entity> Data { get; set; }

        public PagerList(int totalCount, QueryParam param, List<Entity> data)
        {
            Page = param.Page.Value;
            PageSize = param.Size.Value;
            TotalCount = totalCount;
            Data = data;
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Entity this[int index]
        {
            get => Data[index];
            set => Data[index] = value;
        }
    }
}
