using QuanLySinhVienAPI.ModelDTO.GiangVien;
using QuanLySinhVienAPI.ModelDTO.MonHoc;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVienAPI.ModelDTO.LopHoc
{
    public class LopHocDTO
    {
        public int LophocId { get; set; }
        public string NameLophoc { get; set; }

        //public List<MonHocDTO> monhocs { get; set; }
        public int IdMonHoc {  get; set; }

        public string NameMonHoc { get; set; }
    }
}
