using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services;

namespace QLPhongKham.API.Controllers
{
    [Route("phongkham/[controller]")]
    [ApiController]
    public class PhieuKhamBenhController : ControllerBase
    {
        private IPhhieuKhamBenhServices _services;
        private readonly PhongKhamDBContext _context;
        public PhieuKhamBenhController(IPhhieuKhamBenhServices services, PhongKhamDBContext context)
        {
            _services = services;
            _context = context;
        }
        [HttpGet("getPhieuKhamBenh")]
        public async Task<IActionResult> GetPhieuKhamBenh()
        {
            var result = await _services.getPhieuKhamBenh();
            return Ok(result);
        }
        [HttpGet("getPhieuKhamId/{id}")]
        public async Task<IActionResult> GetPhieuKhamId(int id)
        {
            var result = await _services.getPhieuKhamId(id);
            return Ok(result);
        }
        [HttpPost("modify")]
        public async Task<IActionResult> Modify(PhieuKhamBenhMap phieuKhamBenhMap)
        {
            var result = await _services.modify(phieuKhamBenhMap);
            return Ok(result);
        }
             
    }
}
