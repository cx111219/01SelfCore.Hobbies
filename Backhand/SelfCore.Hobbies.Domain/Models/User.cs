using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SelfCore.Hobbies.Domains.Models
{
    [Table("users")]
    public partial class User : IDeleted , Ikey
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(100)]
        public string Psd { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool? Gender { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }
        [StringLength(200)]
        public string Headshot { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public bool? IsAdmin { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Creatime { get; set; }
        [Column(TypeName = "int(11)")]
        public int? Creator { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
