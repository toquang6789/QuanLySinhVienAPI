using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVienAPI.Entities
{
    public class SinhVien
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public int LopId { get; set; }
        [ForeignKey("LopId")]
        [Required]
        public LopHoc lopHocs { get; set; }

        public int IdKhoaHoc { get; set; }
        [ForeignKey("IdKhoaHoc")]
        [Required]
        public KhoaHoc khoaHocs { get; set; }

    }
}
