using QuanLySinhVienAPI.ModelDTO.LopHoc;
using QuanLySinhVienAPI.ModelDTO.MonHoc;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVienAPI.ModelDTO.SinhVien
{
    public class SinhVienDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public int LopId { get; set; }
        public string LopName { get; set; }

        public int IdKhoaHoc { get; set; }
        public string NameKhoaHoc { get; set; }

        public List<MonHocDTO> monhocs { get; set; }
    }
}
