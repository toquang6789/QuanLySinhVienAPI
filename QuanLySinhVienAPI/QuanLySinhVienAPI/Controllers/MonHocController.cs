using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVienAPI.ModelDTO.MonHoc;
using QuanLySinhVienAPI.Service;

namespace QuanLySinhVienAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonHocController : ControllerBase
    {
        public readonly IMHService _mHService;
        public readonly ILogger<MonHocController> _logger;
        public MonHocController(IMHService mHService, ILogger<MonHocController> logger)
        {
            _mHService = mHService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetMH()
        {
            try
            {
                var data = await _mHService.GetMH();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateMH([FromBody] CreateMonHocDTO createMonHocDTO)
        {
            if (!ModelState.IsValid) return Problem("Validate failed");
            try
            {
                await _mHService.CreateMH(createMonHocDTO);
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
        public async Task<IActionResult> DeleteMH(int id)
        {
            try
            {
                await _mHService.DeleteMH(id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product with ID");
            }
            return Ok("Delete Succesfully");
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateMH(int id, UpdateMonHocDTO updateMonHocDTO)
        {
            try
            {
                await _mHService.UpdateMH(id, updateMonHocDTO);
                return Ok("Update Succesfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<MonHocDTO> GetById([FromRoute]int id)
        {
            return await _mHService.GetByID(id);
        }
    }
}
