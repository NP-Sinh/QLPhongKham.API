using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services;

namespace QLPhongKham.API.Controllers
{
    [Route("phongkham/[controller]")]
    [ApiController]
    public class DonThuocController : ControllerBase
    {
        private IDonThuocServices _services;
        private readonly PhongKhamDBContext _context;
        public DonThuocController(IDonThuocServices services, PhongKhamDBContext context)
        {
            _services = services;
            _context = context;
        }
        [HttpGet("getDonThuoc")]
        public async Task<IActionResult> GetDonThuoc()
        {
            var result = await _services.getDonThuoc();
            return Ok(result);
        }
        [HttpGet("getDonThuocId/{id}")]
        public async Task<IActionResult> GetDonThuocId(int id)
        {
            var result = await _services.getDonThuocId(id);
            return Ok(result);
        }
        [HttpPost("createDonThuoc")]
        public async Task<IActionResult> CreateDonThuoc([FromBody] QlDonThuoc data)
        {
            var result = await _services.createDonThuoc(data.donThuocMap, data.chiTietDonThuocMaps);
            return Ok(result);
        }
        [HttpPost("deleteDonThuoc/{id}")]
        public async Task<IActionResult> DeleteDonThuoc(int id)
        {
            var result = await _services.deleteDonThuoc(id);
            return Ok(result);
        }
        [HttpPost("deleteCTDonThuoc/{id}")]
        public async Task<IActionResult> DeleteCTDonThuoc(int id)
        {
            var result = await _services.deleteCTDonThuoc(id);
            return Ok(result);
        }
    }
    public class QlDonThuoc
    {
        public DonThuocMap donThuocMap { get; set; }
        public List<ChiTietDonThuocMap> chiTietDonThuocMaps { get; set; }
    }
}
