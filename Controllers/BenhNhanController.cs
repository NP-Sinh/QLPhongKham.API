using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services;

namespace QLPhongKham.API.Controllers
{
    [Route("phongkham/[controller]")]
    [ApiController]
    public class BenhNhanController : ControllerBase
    {
        private IBenhNhanServices _services;
        private readonly PhongKhamDBContext _context;
        public BenhNhanController(IBenhNhanServices services, PhongKhamDBContext context)
        {
            _services = services;
            _context = context;
        }
        [HttpGet("GetBenhNhan")]
        public async Task<IActionResult> GetBenhNhan()
        {
            var benhNhans = await _services.getBenhNhan();
            return Ok(benhNhans);
        }
        [HttpGet("GetBenhNhanById/{id}")]
        public async Task<IActionResult> GetBenhNhanById(int id)
        {
            var benhNhan = await _services.getBenhNhanId(id);
            return Ok(benhNhan);
        }
        [HttpPost("Modify")]
        public async Task<IActionResult> Modify([FromBody] BenhNhanMap benhNhanMap)
        {
            var result = await _services.modify(benhNhanMap);
            return Ok(result);
        }
    }
}
