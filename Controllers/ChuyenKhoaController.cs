
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services;

namespace QLPhongKham.API.Controllers
{
    [Route("phongkham/[controller]")]
    [ApiController]
    public class ChuyenKhoaController : ControllerBase
    {
        private IChuyenKhoaServices _services;
        private readonly PhongKhamDBContext _context;
        public ChuyenKhoaController(IChuyenKhoaServices services, PhongKhamDBContext context)
        {
            _services = services;
            _context = context;
        }
        [HttpGet("GetChuyenKhoa")]
        public async Task<IActionResult> GetChuyenKhoa()
        {
            var chuyenKhoas = await _services.getChuyenKhoa();
            return Ok(chuyenKhoas);
        }
        [HttpGet("GetChuyenKhoaId/{id}")]
        public async Task<IActionResult> GetChuyenKhoaId(int id)
        {
            var chuyenKhoa = await _services.getChuyenKhoaId(id);
            return Ok(chuyenKhoa);
        }
        [HttpPost("Modify")]
        public async Task<IActionResult> Modify([FromBody] ChuyenKhoaMap chuyenKhoaMap)
        {
            var result = await _services.modify(chuyenKhoaMap);
            return Ok(result);
        }
    }
}
