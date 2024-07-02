using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVienAPI.Entities
{
    public class MonHoc
    {
        [Key]
        public int IdMonHoc { get; set; }
        [Required]
        [MaxLength(100)]
        public string NameMonHoc { get; set; }

        public int IdGiangVien { get; set; }
        [ForeignKey("IdGiangVien")]
        [Required]
        public GiangVien giangViens { get; set; }

        public List<LopHoc>? lopHocs { get; set; }

    }
}
