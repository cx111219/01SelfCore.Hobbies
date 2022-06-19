using Microsoft.EntityFrameworkCore;
using SelfCore.Hobbies.Domains.Models;

namespace SelfCore.Hobbies.Domains
{
    public partial class HobbyContext : DbContext
    {
        public HobbyContext(DbContextOptions<HobbyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Travel> Travels { get; set; }
        public virtual DbSet<User> Users { get; set; }
               
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasComment("书籍");

                entity.Property(e => e.Id).HasComment("主键");

                entity.Property(e => e.Address).HasComment("文档地址");

                entity.Property(e => e.Author).HasComment("作者");

                entity.Property(e => e.BookType).HasComment("类型");

                entity.Property(e => e.Brief).HasComment("简介");

                entity.Property(e => e.Creatime).HasComment("创建时间");

                entity.Property(e => e.Creator).HasComment("创建人");

                entity.Property(e => e.IsDeleted).HasComment("删除");

                entity.Property(e => e.Name).HasComment("书名");

                entity.Property(e => e.Picture).HasComment("插图");
                entity.HasQueryFilter(t => !t.IsDeleted);
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.HasComment("美食");
                entity.HasQueryFilter(t => !t.IsDeleted);
                entity.Property(e => e.Id).HasComment("主键");

                entity.Property(e => e.Creatime).HasComment("创建时间");

                entity.Property(e => e.Creator).HasComment("创建人");

                entity.Property(e => e.IsDeleted).HasComment("删除");

                entity.Property(e => e.Picture).HasComment("图片");

                entity.Property(e => e.Remark).HasComment("感想");

                entity.Property(e => e.TravelId).HasComment("旅游主键");

                entity.Property(e => e.Type).HasComment("来源");
            });

            modelBuilder.Entity<Travel>(entity =>
            {
                entity.HasComment("旅游");
                entity.HasQueryFilter(t => !t.IsDeleted);
                entity.Property(e => e.Id).HasComment("主键");

                entity.Property(e => e.Companion).HasComment("同伴");

                entity.Property(e => e.Cost).HasComment("费用");

                entity.Property(e => e.Creatime).HasComment("创建时间");

                entity.Property(e => e.Creator).HasComment("创建人");

                entity.Property(e => e.IsDeleted).HasComment("删除");

                entity.Property(e => e.Location).HasComment("旅行地点");

                entity.Property(e => e.MianPic).HasComment("主图");

                entity.Property(e => e.Remark).HasComment("感想");

                entity.Property(e => e.TravelDate).HasComment("旅行时间");

                entity.Property(e => e.TravelType).HasComment("类型");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasComment("用户");
                entity.HasQueryFilter(t => !t.IsDeleted);
                entity.Property(e => e.Id).HasComment("主键");

                entity.Property(e => e.Birthday).HasComment("生日");

                entity.Property(e => e.Code).HasComment("用户名");

                entity.Property(e => e.Creatime).HasComment("创建时间");

                entity.Property(e => e.Creator).HasComment("创建人");

                entity.Property(e => e.Gender).HasComment("性别");

                entity.Property(e => e.Headshot).HasComment("头像");

                entity.Property(e => e.Headshot).HasComment("email");

                entity.Property(e => e.IsAdmin).HasComment("是否管理员");

                entity.Property(e => e.IsDeleted).HasComment("删除");

                entity.Property(e => e.Name).HasComment("昵称");

                entity.Property(e => e.Psd).HasComment("密码");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
