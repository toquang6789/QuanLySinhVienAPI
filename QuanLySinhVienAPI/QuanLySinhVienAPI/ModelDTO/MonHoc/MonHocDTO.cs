using QuanLySinhVienAPI.ModelDTO.GiangVien;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVienAPI.ModelDTO.MonHoc
{
    public class MonHocDTO
    {
        public int IdMonHoc { get; set; }
        public string NameMonHoc { get; set; }
       // public List<GiangVienDTO> giangviens { get; set; }
        public int idGiangVien {  get; set; }
        public string NameGiangVien { get; set; }

    }
}
