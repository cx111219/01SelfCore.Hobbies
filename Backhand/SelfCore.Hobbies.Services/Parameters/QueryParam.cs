namespace SelfCore.Hobbies.Services
{
    /// <summary>
    /// 检索对象
    /// </summary>
    public partial class QueryParam
    {
        /// <summary>
        /// 检索关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 类型检索
        /// </summary>
        public int? Type { get; set; } = 0;
        public int? Size { get; set; } = 10;
        public int? Page { get; set; } = 1;
        public string Order { get; set; } = "Id desc";
    }
}
