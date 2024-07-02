using Microsoft.EntityFrameworkCore;
using QuanLySinhVienAPI.Entities;
using QuanLySinhVienAPI.ModelDTO.LopHoc;

namespace QuanLySinhVienAPI.Service
{
    public interface ILHService
    {
        Task CreateLH(CreateLopHocDTO createLopHocDTO);
        Task UpdateLH(int id,UpdateLopHocDTO updateLopHocDTO);
        Task DeleteLH(int id);
        Task<List<LopHocDTO>> GetAll();
        Task<LopHocDTO> GetById(int id);
    }
    public class LopHocService : ILHService
    {
        public readonly IRepository<LopHoc> _lopHocRepository;
        public readonly IRepository<MonHoc> _monHocRepository;
        public LopHocService(IRepository<LopHoc> lopHocRepository, IRepository<MonHoc> monHocRepository)
        {
             _lopHocRepository = lopHocRepository;
             _monHocRepository = monHocRepository;
        }
        public async Task CreateLH(CreateLopHocDTO createLopHocDTO)
        {
            var lophoc = new LopHoc
            {
                NameLop = createLopHocDTO.CreateNameLophoc,
                IdMonHoc = createLopHocDTO.IdMonHoc,
            };
            await _lopHocRepository.Add(lophoc);
        }

        public async Task DeleteLH(int id)
        {
            var lophoc = await _lopHocRepository.GetById(id);
            if(lophoc == null) throw new Exception("MH not found");
            await _lopHocRepository.Delete(lophoc);
        }

        public async Task<List<LopHocDTO>> GetAll()
        {
            var result = from lh in _lopHocRepository.GetAll()
                         join mh in _monHocRepository.GetAll()
                         on lh.IdMonHoc equals mh.IdMonHoc
                         select new LopHocDTO
                         {
                             LophocId = lh.LopId,
                             NameLophoc = lh.NameLop,
                             IdMonHoc= lh.IdMonHoc,
                             NameMonHoc = mh.NameMonHoc,
                         };
            return await result.ToListAsync();
        }

        public async Task<LopHocDTO> GetById(int id)
        {
            var lophoc = await _lopHocRepository.GetById(id);
            var monhoc = await _monHocRepository.GetById(id);
            return new LopHocDTO
            {
                LophocId = lophoc.LopId,
                NameLophoc = lophoc.NameLop,
                IdMonHoc = lophoc.IdMonHoc,
                NameMonHoc = monhoc.NameMonHoc,
            };
        }

        public async Task UpdateLH(int id,UpdateLopHocDTO updateLopHocDTO)
        {
            var lophoc = await _lopHocRepository.GetById(id);
            if(lophoc == null) throw new Exception("MH not found");
            lophoc.NameLop = updateLopHocDTO.CreateNameLophoc;
            lophoc.IdMonHoc = updateLopHocDTO.IdMonHoc;
            await _lopHocRepository.Update(lophoc);
        }
    }
}
