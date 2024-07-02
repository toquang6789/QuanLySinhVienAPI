using Microsoft.EntityFrameworkCore;

namespace QuanLySinhVienAPI.Entities
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions) { }

        DbSet<SinhVien> sinhViens { get; set; }
        DbSet<MonHoc> monHocs { get; set; }
        DbSet<GiangVien> giangViens { get; set; }
        DbSet<LopHoc> lopHocs { get; set; }
        DbSet<KhoaHoc> khoaHocs { get; set; }
    }
}
