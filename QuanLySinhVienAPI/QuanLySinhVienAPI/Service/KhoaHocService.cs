using Microsoft.EntityFrameworkCore;
using QuanLySinhVienAPI.Entities;
using QuanLySinhVienAPI.ModelDTO.GiangVien;
using QuanLySinhVienAPI.ModelDTO.KhoaHoc;

namespace QuanLySinhVienAPI.Service
{
    public interface IKHService
    {
        Task CreateKH(CreateKhoaHocDTO createKhoaHocDTO);
        Task UpdateKH(int id,UpdateKhoaHocDTO updateKhoaHocDTO);
        Task DeleteKH(int id);
        Task<List<KhoaHocDTO>> GetKH();
        Task<KhoaHocDTO> GetByID(int id);
    }
    public class KhoaHocService : IKHService
    {
        public readonly IRepository<KhoaHoc> _khoahocRepository;
        public KhoaHocService(IRepository<KhoaHoc> khoahocRepository)
        {
            _khoahocRepository = khoahocRepository;
        }
        public async Task CreateKH(CreateKhoaHocDTO createKhoaHocDTO)
        {
            var khoahoc = new KhoaHoc
            {
                NameKhoaHoc = createKhoaHocDTO.createNamKhoaHoc
            };
            await _khoahocRepository.Add(khoahoc);
        }

        public async Task DeleteKH(int id)
        {
            var khoahoc = await _khoahocRepository.GetById(id);
            if (khoahoc == null) throw new Exception("GV not found");
            await _khoahocRepository.Delete(khoahoc);
        }

        public async Task<KhoaHocDTO> GetByID(int id)
        {
            var khoahoc = await _khoahocRepository.GetById(id);
            if (khoahoc == null)
            {
                throw new Exception("GV not found");
            }
            return new KhoaHocDTO
            {
                IdKhoaHoc = khoahoc.IdKhoaHoc,
             NameKhoaHoc = khoahoc.NameKhoaHoc,    
            };
        }

        public async Task<List<KhoaHocDTO>> GetKH()
        {
            var khachang = from kh in _khoahocRepository.GetAll()
                           select new KhoaHocDTO
                           {
                               IdKhoaHoc = kh.IdKhoaHoc,
                               NameKhoaHoc = kh.NameKhoaHoc
                           };
            return await khachang.ToListAsync();
        }

        public async Task UpdateKH(int id, UpdateKhoaHocDTO updateKhoaHocDTO)
        {
            var khoahoc = await _khoahocRepository.GetById(id);
            if(khoahoc == null) throw new Exception("GV not found");
            khoahoc.NameKhoaHoc = updateKhoaHocDTO.createNamKhoaHoc;
            await _khoahocRepository.Update(khoahoc);
        }
    }
}
