using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVienAPI.Entities
{
    public class LopHoc
    {
        [Key]
        public int LopId { get; set; }
        [Required]
        [MaxLength(50)]
        public string NameLop { get; set; }

        public List<SinhVien>? sinhViens { get; set; }

        public int IdMonHoc { get; set; }
        [ForeignKey("IdMonHoc")]
        [Required]
        public MonHoc monHoc { get; set; }

    }
}
