using Microsoft.EntityFrameworkCore;
using QuanLySinhVienAPI.Entities;
using QuanLySinhVienAPI.ModelDTO.GiangVien;
using QuanLySinhVienAPI.ModelDTO.MonHoc;
using QuanLySinhVienAPI.ModelDTO.SinhVien;

namespace QuanLySinhVienAPI.Service
{
    public interface ISVService
    {
        Task CreateSV(CreateSinhVienDTO createSinhVienDTO);
        Task UpdateSV(int id, UpdateSinhVienDTO updateSinhVienDTO);
        Task DeleteSV(int id);
        Task<List<ModelSinhVienDTO>> GetSV();
        Task<ModelSinhVienDTO> GetByID(int id);
    }
    public class SinhVienService : ISVService
    {
        private readonly IRepository<LopHoc> _lopHocRepository;
        private readonly IRepository<KhoaHoc> _khoahocRepository;
        private readonly IRepository<SinhVien> _sinhvienHocRepository;
        private readonly IRepository<MonHoc> _monhocRepository;
        private readonly IRepository<GiangVien> _giangvienRepository;
        public SinhVienService(IRepository<LopHoc> lopHocRepository, IRepository<SinhVien> sinhvienHocRepository, IRepository<KhoaHoc> khoahocRepository,
            IRepository<MonHoc> monhocRepository, IRepository<GiangVien> giangvienRepository)
        {
            _lopHocRepository = lopHocRepository;
            _sinhvienHocRepository = sinhvienHocRepository;
            _khoahocRepository = khoahocRepository;
            _monhocRepository = monhocRepository;
            _giangvienRepository = giangvienRepository;
        }
        public async Task CreateSV(CreateSinhVienDTO createSinhVienDTO)
        {
            var sinhvien = new SinhVien
            {
                Name = createSinhVienDTO.Name,
                Description = createSinhVienDTO.Description,
                LopId = createSinhVienDTO.LopId,
                IdKhoaHoc = createSinhVienDTO.IdKhoaHoc
            };
            await _sinhvienHocRepository.Add(sinhvien);
        }

        public async Task DeleteSV(int id)
        {
            var sinhvien = await _sinhvienHocRepository.GetById(id);
            if (sinhvien == null)
            {
                throw new Exception("SV not found");
            }
            await _sinhvienHocRepository.Delete(sinhvien);
        }

        public async Task<ModelSinhVienDTO> GetByID(int id)
        {
            var sv = await _sinhvienHocRepository.GetById(id);
            var lop = await _lopHocRepository.GetAll().FirstAsync(l => l.LopId == sv.LopId);
            var khoahoc = await _khoahocRepository.GetAll().FirstAsync(kh => kh.IdKhoaHoc == sv.IdKhoaHoc);
            var monhoc = await _monhocRepository.GetAll().FirstAsync(mh => mh.IdMonHoc == lop.IdMonHoc);
            var giangvien = await _giangvienRepository.GetAll().FirstAsync(gv => gv.IdGiangVien == monhoc.IdGiangVien);
            return new ModelSinhVienDTO
            {
               Id = sv.Id,
               Name = sv.Name,
                Description = sv.Description,
               LopId = sv.LopId,
               LopName = lop.NameLop,
               IdKhoaHoc = sv.IdKhoaHoc,
               NameKhoaHoc = khoahoc.NameKhoaHoc,
               idMonHoc = lop.IdMonHoc,
               NameMonHoc = monhoc.NameMonHoc,
               idGiangVien = monhoc.IdGiangVien,
               NameGiangVien = giangvien.NameGiangVien
            };
        }

        private async Task<ModelSinhVienDTO> SVInfo(int id)
        {
            var sv = await _sinhvienHocRepository.GetAll().FirstAsync(s => s.Id == id);
            var lop = await _lopHocRepository.GetAll().FirstAsync(l => l.LopId == sv.LopId);
            var khoahoc = await _khoahocRepository.GetAll().FirstAsync(kh => kh.IdKhoaHoc == sv.IdKhoaHoc);
            var monhoc = await _monhocRepository.GetAll().FirstAsync(mh => mh.IdMonHoc == lop.IdMonHoc);
            var giangvien = await _giangvienRepository.GetAll().FirstAsync(gv => gv.IdGiangVien == monhoc.IdGiangVien);
            return new ModelSinhVienDTO
            {
                Id = id,
                Name = sv.Name,
                Description = sv.Description,
                LopId = lop.LopId,
                LopName = lop.NameLop,
                IdKhoaHoc = khoahoc.IdKhoaHoc,
                NameKhoaHoc = khoahoc.NameKhoaHoc,
                idMonHoc = monhoc.IdMonHoc,
                NameMonHoc = monhoc.NameMonHoc,
                idGiangVien = giangvien.IdGiangVien,
                NameGiangVien = giangvien.NameGiangVien
            };
        }

        public async Task<List<ModelSinhVienDTO>> GetSV()
        {
            var ids = await (from sv in _sinhvienHocRepository.GetAll()
                             select sv.Id).ToListAsync();

            var result = new List<ModelSinhVienDTO>();
            /// ids.ForEach(async id => result.Add(await SVInfo(id)));
            foreach (var id in ids)
            {
                var svinfo = await SVInfo(id);
                result.Add(svinfo);
            }
            return result;
        }

        public async Task UpdateSV(int id, UpdateSinhVienDTO updateSinhVienDTO)
        {
            var sinhvien = await _sinhvienHocRepository.GetById(id);
            if (sinhvien == null)
            {
                throw new Exception("SV not found");
            }
            sinhvien.Name = updateSinhVienDTO.Name;
            sinhvien.Description = updateSinhVienDTO.Description;
            sinhvien.LopId = updateSinhVienDTO.LopId;
            sinhvien.IdKhoaHoc = updateSinhVienDTO.IdKhoaHoc;
            await _sinhvienHocRepository.Update(sinhvien);

        }
    }
}
