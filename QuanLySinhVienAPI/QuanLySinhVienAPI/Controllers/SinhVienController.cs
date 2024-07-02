using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVienAPI.ModelDTO.SinhVien;
using QuanLySinhVienAPI.Service;

namespace QuanLySinhVienAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhVienController : ControllerBase
    {
        private readonly ISVService _sVService;
        private readonly ILogger<SinhVienController> _logger;
        public SinhVienController(ISVService sVService, ILogger<SinhVienController> logger)
        {
            _sVService = sVService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _sVService.GetSV();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateSV([FromBody] CreateSinhVienDTO createSinhVienDTO)
        {
            if(!ModelState.IsValid)
            {
                return Problem("Validate failed");
            }
            try
            {
                await _sVService.CreateSV(createSinhVienDTO);
                return Ok("Create succesfully");
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
        public async Task<ModelSinhVienDTO> GetByID([FromRoute] int id)
        {
            return await _sVService.GetByID(id);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sVService.DeleteSV(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product with ID");
            }
            return Ok("Delete Succesfully");
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, UpdateSinhVienDTO update)
        {
            try
            {
                await _sVService.UpdateSV(id, update);
                return Ok("Update succefully");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }



    }
}
