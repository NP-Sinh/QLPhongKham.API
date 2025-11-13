using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services;

namespace QLPhongKham.API.Controllers
{
    [Route("phongkham/[controller]")]
    [ApiController]
    public class LichHenController : ControllerBase
    {
        private ILichHenServices _services;
        private readonly PhongKhamDBContext _context;
        public LichHenController(ILichHenServices services, PhongKhamDBContext context)
        {
            _services = services;
            _context = context;
        }
        [HttpGet("GetLichHen")]
        public async Task<IActionResult> GetLichHen()
        {
            var result = await _services.getLichHen();
            return Ok(result);
        }
        [HttpGet("GetLichHenId/{id}")]
        public async Task<IActionResult> GetLichHenId(int id)
        {
            var result = await _services.getLichHenId(id);
            return Ok(result);
        }
        [HttpPost("Modify")]
        public async Task<IActionResult> Modify([FromBody] LichHenMap lichHenMap)
        {
            var result = await _services.modify(lichHenMap);
            return Ok(result);
        }
    }
}
