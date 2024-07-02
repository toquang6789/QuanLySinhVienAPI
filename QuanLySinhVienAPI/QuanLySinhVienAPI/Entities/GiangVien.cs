using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVienAPI.Entities
{
    public class GiangVien
    {
        [Key]
        public int IdGiangVien { get; set; }

        [Required]
        [MaxLength(100)]
        public string NameGiangVien { get; set; }

        public List<MonHoc>? monHocs { get; set; }

    }
}
