using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVienAPI.Entities
{
    public class KhoaHoc
    {
        [Key]
        public int IdKhoaHoc { get; set; }
        [Required]
        [MaxLength(100)]
        public string NameKhoaHoc { get; set; }
        public List<SinhVien>? sinhViens { get; set; }
    }
}
