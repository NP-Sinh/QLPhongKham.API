using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Services;

namespace QLPhongKham.API.Controllers
{
    [Route("phongkham/[controller]")]
    [ApiController]
    public class ThuocController : ControllerBase
    {
        private IThuocServices _services;
        private readonly PhongKhamDBContext _context;
        public ThuocController(IThuocServices services, PhongKhamDBContext context)
        {
            _services = services;
            _context = context;
        }
        [HttpGet("GetThuoc")]
        public async Task<IActionResult> GetThuoc()
        {
            var thuocs = await _services.getThuoc();
            return Ok(thuocs);
        }
        [HttpGet("GetThuocId/{id}")]
        public async Task<IActionResult> GetThuocId(int id)
        {
            var thuoc = await _services.getThuocId(id);
            return Ok(thuoc);

        }
        [HttpPost("Modify")]
        public async Task<IActionResult> Modify([FromBody] Models.Map.ThuocMap thuocMap)
        {
            var result = await _services.modify(thuocMap);
            return Ok(result);
        }
    }
}
