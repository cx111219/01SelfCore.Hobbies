using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SelfCore.Hobbies.Domains.Models
{
    [Table("foods")]
    public partial class Food : IDeleted, Ikey
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Picture { get; set; }
        [Column(TypeName = "tinyint(4)")]
        public sbyte Type { get; set; }
        [Column(TypeName = "int(11)")]
        public int? TravelId { get; set; }
        [Column(TypeName = "tinytext")]
        public string Remark { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Creatime { get; set; }
        [Column(TypeName = "int(11)")]
        public int? Creator { get; set; }
        public bool IsDeleted { get; set; }
    }
}
