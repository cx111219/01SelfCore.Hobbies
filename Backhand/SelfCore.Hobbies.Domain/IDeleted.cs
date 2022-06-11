namespace SelfCore.Hobbies.Domains
{
    /// <summary>
    /// 删除
    /// </summary>
    public interface IDeleted
    {
        public System.DateTime? Creatime { get; set; }
        public int? Creator { get; set; }
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// 主键
    /// </summary>
    public interface Ikey
    {
        public int Id { get; set; }
    }
}
