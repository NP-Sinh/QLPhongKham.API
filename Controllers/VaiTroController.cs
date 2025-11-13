using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services;

namespace QLPhongKham.API.Controllers
{
    [Route("phongkham/[controller]")]
    [ApiController]
    public class VaiTroController : ControllerBase
    {
        private readonly PhongKhamDBContext _context;
        private readonly IVaiTroServices _services;
        public VaiTroController(PhongKhamDBContext context, IVaiTroServices services)
        {
            _context = context;
            _services = services;
        }
        [HttpGet("getVaiTro")]
        public async Task<IActionResult> getVaiTro()
        {
            var result = await _services.getVaiTro();
            return Ok(result);
        }
        [HttpGet("getVaiTroId/{id}")]
        public async Task<IActionResult> getVaiTroId(int id)
        {
            var result = await _services.getVaiTroId(id);
            return Ok(result);
        }
        [HttpPost("modify")]
        public async Task<IActionResult> modify([FromBody] VaiTroMap vaiTroMap)
        {
            var result = await _services.modify(vaiTroMap);
            return Ok(result);
        }
    }
}
