using Microsoft.EntityFrameworkCore;
using QuanLySinhVienAPI.Entities;
using QuanLySinhVienAPI.ModelDTO.MonHoc;
using QuanLySinhVienAPI.ModelDTO.SinhVien;

namespace QuanLySinhVienAPI.Service
{
    public interface IMHService
    {
        Task CreateMH(CreateMonHocDTO createMonHocDTO);
        Task UpdateMH(int id, UpdateMonHocDTO updateMonHocDTO);
        Task DeleteMH(int id);
        Task<List<MonHocDTO>> GetMH();
        Task<MonHocDTO> GetByID(int id);
    }
    public class MonHocService : IMHService
    {
        private readonly IRepository<MonHoc> _monhocrepository;
        private readonly IRepository<GiangVien> _giangvienrepository;

        public MonHocService(IRepository<MonHoc> monhocrepository, IRepository<GiangVien> giangvienrepository)
        {
            _giangvienrepository = giangvienrepository;
            _monhocrepository = monhocrepository;
        }

        public async Task CreateMH(CreateMonHocDTO createMonHocDTO)
        {
            var monhoc = new MonHoc
            {
                NameMonHoc = createMonHocDTO.NameCreateMonHoc,
                IdGiangVien = createMonHocDTO.GiangVienID
            };
            await _monhocrepository.Add(monhoc);
        }

        public async Task DeleteMH(int id)
        {
            var monhoc = await _monhocrepository.GetById(id);
            if (monhoc == null)
            {
                throw new Exception("MH not found");
            }
            await _monhocrepository.Delete(monhoc);
        }

        public async Task<MonHocDTO> GetByID(int id)
        {
            var monhoc = await _monhocrepository.GetById(id);
            var giangvien = await _giangvienrepository.GetById(id);
            return new MonHocDTO
            {
                IdMonHoc = monhoc.IdMonHoc,
                NameMonHoc = monhoc.NameMonHoc,
                idGiangVien = monhoc.IdGiangVien,
                NameGiangVien = giangvien.NameGiangVien
            };
        }

        public async Task<List<MonHocDTO>> GetMH()
        {
            var result = from mh in _monhocrepository.GetAll()
                         join gv in _giangvienrepository.GetAll()
                         on mh.IdGiangVien equals gv.IdGiangVien
                         select new MonHocDTO
                         {
                             IdMonHoc = mh.IdMonHoc,
                             NameMonHoc = mh.NameMonHoc,
                             idGiangVien = mh.IdGiangVien,
                             NameGiangVien = gv.NameGiangVien
                         };
            return await result.ToListAsync();
        }

        public async Task UpdateMH(int id, UpdateMonHocDTO updateMonHocDTO)
        {
            var monhoc = await _monhocrepository.GetById(id);
            if (monhoc == null)
            {
                throw new Exception("MH not found");
            }
            monhoc.NameMonHoc = updateMonHocDTO.NameCreateMonHoc;
            monhoc.IdGiangVien = updateMonHocDTO.GiangVienID;
            await _monhocrepository.Update(monhoc);
        }
    }
}
