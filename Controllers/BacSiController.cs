using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services;

namespace QLPhongKham.API.Controllers
{
    [Route("phongkham/[controller]")]
    [ApiController]
    public class BacSiController : ControllerBase
    {
        private IBacSiServices _services;
        private readonly PhongKhamDBContext _context;
        public BacSiController(IBacSiServices services, PhongKhamDBContext context)
        {
            _services = services;
            _context = context;
        }
        [HttpGet("GetBacSi")]
        public async Task<IActionResult> GetBacSi()
        {
            var bacSis = await _services.getBacSi();
            return Ok(bacSis);
        }
        [HttpGet("GetBacSiById/{id}")]
        public async Task<IActionResult> GetBacSiById(int id)
        {
            var bacSi = await _services.getBacSiId(id);
            return Ok(bacSi);
        }
        [HttpPost("Modify")]
        public async Task<IActionResult> Modify([FromBody] BacSiMap bacSiMap)
        {
            var result = await _services.modify(bacSiMap);
            return Ok(result);
        }
    }
}
