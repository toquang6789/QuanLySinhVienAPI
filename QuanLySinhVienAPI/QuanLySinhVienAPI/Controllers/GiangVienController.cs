using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVienAPI.ModelDTO.GiangVien;
using QuanLySinhVienAPI.ModelDTO.SinhVien;
using QuanLySinhVienAPI.Service;

namespace QuanLySinhVienAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiangVienController : ControllerBase
    {
        private readonly IGVService _iGVService;
        private readonly ILogger<GiangVienController> _logger;

        public GiangVienController(IGVService iGVService, ILogger<GiangVienController> logger)
        {
            _iGVService = iGVService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetGV()
        {
            try
            {
               var data = await _iGVService.GetGV();
                return Ok(data);    
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<GiangVienDTO> GetByID([FromRoute] int id)
        {
            return await _iGVService.GetByID(id);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateGV([FromBody] CreateGiangVienDTO createGiangVienDTO)
        {
            if(!ModelState.IsValid) return Problem("Validate failed");
            try
            {
                await _iGVService.CreateGV(createGiangVienDTO);
                return Ok("Create Succesfully");
            }
            catch (DbUpdateException dbEx)
            {
                // Lấy thông tin chi tiết từ lỗi bên trong (inner exception)
                var innerExceptionMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                return StatusCode(500, $"Lỗi server nội bộ: {innerExceptionMessage}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteGV(int id)
        {
            try
            {
                await _iGVService.DeleteGV(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product with ID");
            }
            return Ok("Delete Succesfully");
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateGV(int id, UpdateGiangVienDTO updateGiangVienDTO)
        {
            try
            {
                await _iGVService.UpdateGV(id, updateGiangVienDTO);
                return Ok("Update Succesfully");
            }
            catch (Exception ex)
            {
                    return StatusCode(500, $"{ex.Message}");   
            }
        }
    }
}
