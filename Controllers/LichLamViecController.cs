using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services;

namespace QLPhongKham.API.Controllers
{
    [Route("phongkham/[controller]")]
    [ApiController]
    public class LichLamViecController : ControllerBase
    {
        private ILichLamViecServices _services;
        public LichLamViecController(ILichLamViecServices services)
        {
            _services = services;
        }
        [HttpGet("getLLV")]
        public async Task<IActionResult> GetLLV()
        {
            var result = await _services.getLichLamViec();
            return Ok(result);
        }
        [HttpGet("getLLVId/{id}")]
        public async Task<IActionResult> GetLLVId(int id)
        {
            var result = await _services.getLichLamViecId(id);
            return Ok(result);
        }
        [HttpPost("modify")]
        public async Task<IActionResult> Modify(LichLamViecMap lichLamViecMap)
        {
            var result = await _services.modify(lichLamViecMap);
            return Ok(result);
        }
    }
}
