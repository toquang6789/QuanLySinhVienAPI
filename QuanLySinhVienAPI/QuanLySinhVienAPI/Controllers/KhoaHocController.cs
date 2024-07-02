using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVienAPI.Entities;
using QuanLySinhVienAPI.ModelDTO.KhoaHoc;
using QuanLySinhVienAPI.Service;

namespace QuanLySinhVienAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaHocController : ControllerBase
    {
        private readonly IKHService _iKHService;
        private readonly ILogger<KhoaHocController> _logger;

        public KhoaHocController(IKHService iKHService, ILogger<KhoaHocController> logger)
        {
            _iKHService = iKHService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetKH()
        {
            try
            {
                var data = await _iKHService.GetKH();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateKH([FromBody] CreateKhoaHocDTO createKhoaHocDTO)
        {
            if (!ModelState.IsValid) return Problem("Validate failed");
            try
            {
                await _iKHService.CreateKH(createKhoaHocDTO);
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
        [HttpGet("{id}")]
        public async Task<KhoaHocDTO> GetID([FromRoute] int id)
        {
            return await _iKHService.GetByID(id);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteKH(int id)
        {
            try
            {
                await _iKHService.DeleteKH(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product with ID");
            }
            return Ok("Delete Succesfully");
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateKH(int id, UpdateKhoaHocDTO updateKhoaHocDTO)
        {
            try
            {
                await _iKHService.UpdateKH(id, updateKhoaHocDTO);
                return Ok("Update Succesfully");
            }
            catch (Exception x)
            {
                return StatusCode(500, $"{x.Message}");
            }
        }
    }
}
