using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services;

namespace QLPhongKham.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhongkhamController : ControllerBase
    {
        private IPhongKhamServices _services;
        private readonly PhongKhamDBContext _context;
        public PhongkhamController(IPhongKhamServices services, PhongKhamDBContext context)
        {
            _services = services;
            _context = context;
        }
        [HttpGet("GetPhongKham")]
        public async Task<IActionResult> GetPhongKham()
        {
            var phongKhams = await _services.getPhongKham();
            return Ok(phongKhams);
        }
        [HttpGet("GetPhongKhamId/{id}")]
        public async Task<IActionResult> GetPhongKhamId(int id)
        {
            var phongKham = await _services.getPhongKhamId(id);
            return Ok(phongKham);
        }
        [HttpPost("Modify")]
        public async Task<IActionResult> Modify([FromBody] PhongKhamMap phongKhamMap)
        {
            var result = await _services.modify(phongKhamMap);
            return Ok(result);
        }
    }
}
