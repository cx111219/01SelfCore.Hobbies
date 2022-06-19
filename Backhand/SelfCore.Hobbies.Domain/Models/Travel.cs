using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SelfCore.Hobbies.Domains.Models
{
    [Table("travels")]
    public partial class Travel : IDeleted, Ikey
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Column(TypeName = "tinyint(4)")]
        public sbyte TravelType { get; set; }
        [StringLength(100)]
        public string MianPic { get; set; }
        [StringLength(2000)]
        public string SubPics { get; set; }
        [Required]
        [StringLength(50)]
        public string Location { get; set; }
        [Column(TypeName = "date")]
        public DateTime TravelDate { get; set; }
        [StringLength(50)]
        public string Companion { get; set; }
        [Column(TypeName = "float(8,2)")]
        public float? Cost { get; set; }
        [Column(TypeName = "tinytext")]
        public string Remark { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Creatime { get; set; }
        [Column(TypeName = "int(11)")]
        public int? Creator { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsDeleted { get; set; }
    }
}
