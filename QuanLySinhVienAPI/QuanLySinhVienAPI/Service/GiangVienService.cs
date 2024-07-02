using Microsoft.EntityFrameworkCore;
using QuanLySinhVienAPI.Entities;
using QuanLySinhVienAPI.ModelDTO.GiangVien;
using QuanLySinhVienAPI.ModelDTO.SinhVien;

namespace QuanLySinhVienAPI.Service
{
    public interface IGVService
    {
        Task CreateGV(CreateGiangVienDTO createGiangVienDTO);
        Task UpdateGV(int id,UpdateGiangVienDTO updateGiangVienDTO);
        Task DeleteGV(int id);
        Task<List<GiangVienDTO>> GetGV();
        Task<GiangVienDTO> GetByID(int id);
    }
    public class GiangVienService : IGVService
    {
        public readonly IRepository<GiangVien> _giangvienRepository;
        public GiangVienService(IRepository<GiangVien> giangvienRepository)
        {
            _giangvienRepository = giangvienRepository;
        }
        public async Task CreateGV(CreateGiangVienDTO createGiangVienDTO)
        {
            var giangvien = new GiangVien
            {
                NameGiangVien = createGiangVienDTO.CreateNameGiangVien
            };
            await _giangvienRepository.Add(giangvien);
        }

        public async Task DeleteGV(int id)
        {
            var giangvien = await _giangvienRepository.GetById(id);
            if (giangvien == null)
            {
                throw new Exception("SV not found");
            }
            await _giangvienRepository.Delete(giangvien);
        }
        public async Task<GiangVienDTO> GetByID(int id)
        {
            var giangvien = await _giangvienRepository.GetById(id);
            if (giangvien == null) {
                throw new Exception("GV not found");
            }
            return new GiangVienDTO
            {
                IdGiangVien = giangvien.IdGiangVien,
                NameGiangVien = giangvien.NameGiangVien
            };
        }

        public async Task<List<GiangVienDTO>> GetGV()
        {
            var resuft = from gv in _giangvienRepository.GetAll()
                         select new GiangVienDTO
                         {
                             IdGiangVien = gv.IdGiangVien,
                             NameGiangVien = gv.NameGiangVien
                         };
            return await resuft.ToListAsync();
        }

        public async Task UpdateGV(int id, UpdateGiangVienDTO updateGiangVienDTO)
        {
            var giangvien = await _giangvienRepository.GetById(id);
            if (giangvien == null) {
                throw new Exception("GV not found");
            }
            giangvien.NameGiangVien = updateGiangVienDTO.CreateNameGiangVien;
            await _giangvienRepository.Update(giangvien);
        }
    }
}
