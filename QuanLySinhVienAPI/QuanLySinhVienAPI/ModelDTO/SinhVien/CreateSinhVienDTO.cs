namespace QuanLySinhVienAPI.ModelDTO.SinhVien
{
    public class CreateSinhVienDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LopId { get; set; }
        public int IdKhoaHoc { get; set; }
    }
}
