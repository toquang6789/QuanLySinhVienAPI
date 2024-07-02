using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVienAPI.ModelDTO.LopHoc;
using QuanLySinhVienAPI.Service;

namespace QuanLySinhVienAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopHocController : ControllerBase
    {
        public readonly ILHService _lhService;
        public readonly ILogger<LopHocController> _logger;

        public LopHocController(ILHService lhService, ILogger<LopHocController> logger)
        {
            _lhService = lhService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetLH()
        {
            try
            {
                var data = await _lhService.GetAll();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateLH([FromBody]CreateLopHocDTO createLopHocDTO)
        {
            if(!ModelState.IsValid) return Problem("Validate failed");
            try
            {
                await _lhService.CreateLH(createLopHocDTO);
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
        public async Task<LopHocDTO> GetById([FromRoute] int id)
        {
             return await _lhService.GetById(id); 
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _lhService.DeleteLH(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product with ID");
            }
            return Ok("Delete Succesfully");
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id,UpdateLopHocDTO updateLopHocDTO)
        {
            try
            {
                await _lhService.UpdateLH(id, updateLopHocDTO);
                return Ok("Update Succesfully");
            }catch(Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
