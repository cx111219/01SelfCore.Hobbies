using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SelfCore.Hobbies.Domains.Models
{
    [Table("books")]
    public partial class Book : IDeleted ,Ikey
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Author { get; set; }
        [Column(TypeName = "tinyint(4)")]
        public sbyte? BookType { get; set; }
        [StringLength(200)]
        public string Picture { get; set; }
        [Column(TypeName = "tinytext")]
        public string Brief { get; set; }
        [StringLength(200)]
        public string Adress { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Creatime { get; set; }
        [Column(TypeName = "int(11)")]
        public int? Creator { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsDeleted { get; set; }
    }
}
