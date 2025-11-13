using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services;

namespace QLPhongKham.API.Controllers
{
    [Route("phongkham/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private INguoiDungServices _services;
        private readonly PhongKhamDBContext _context;
        public NguoiDungController(INguoiDungServices services, PhongKhamDBContext context)
        {
            _services = services;
            _context = context;
        }
        [HttpGet("GetNguoiDung")]
        public async Task<IActionResult> GetNguoiDung()
        {
            var nguoiDungs = await _services.getNguoiDung();
            return Ok(nguoiDungs);
        }
        [HttpGet("GetNguoiDungId/{id}")]
        public async Task<IActionResult> GetNguoiDungId(int id)
        {
            var nguoiDung = await _services.getNguoiDungId(id);
            return Ok(nguoiDung);
        }

        [HttpPost("Modify")]
        public async Task<IActionResult> Modify([FromBody] NguoiDungMap nguoiDungMap)
        {
            var result = await _services.modify(nguoiDungMap);
            return Ok(result);
        }
    }
}
