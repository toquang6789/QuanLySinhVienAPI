using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVienAPI.ModelDTO.KhoaHoc
{
    public class KhoaHocDTO
    {
        public int IdKhoaHoc { get; set; }
        [Required]
        [MaxLength(100)]
        public string NameKhoaHoc { get; set; }
    }
}
