using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongKham.API.Services.AuthServices;

namespace QLPhongKham.API.Controllers
{
    [Route("phongkham/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthServices _services;
        public AuthController(IAuthServices services)
        {
            _services = services;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _services.Login(request.TenDangNhap, request.MatKhau, request.IdVaiTro);
            return Ok(result);
        }
    }
    public class LoginRequest
    {
        public string TenDangNhap { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public int IdVaiTro { get; set; }
    }
}
