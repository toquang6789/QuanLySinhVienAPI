namespace QuanLySinhVienAPI.ModelDTO.SinhVien
{
    public class ModelSinhVienDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public int LopId { get; set; }
        public string LopName { get; set; }

        public int IdKhoaHoc { get; set; }
        public string NameKhoaHoc { get; set; }
        public int idMonHoc { get; set; }
        public string NameMonHoc { get; set; }
        public int idGiangVien { get; set; }
        public string NameGiangVien { get; set; }
    }
}
