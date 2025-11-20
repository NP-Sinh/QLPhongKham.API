using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;

namespace QLPhongKham.API.Services.AuthServices
{
    public interface IAuthServices
    {
        Task<dynamic> Login(string tenDangNhap, string matKhau, int idVaiTro);
    }
    public class AuthServices : IAuthServices
    {
        private readonly PhongKhamDBContext _context;
        private readonly JwtServices _jwtServices;
        private readonly IMapper _mapper;

        public AuthServices(PhongKhamDBContext context, JwtServices jwtServices, IMapper mapper)
        {
            _context = context;
            _jwtServices = jwtServices;
            _mapper = mapper; 
        }

        public async Task<dynamic> Login(string tenDangNhap, string matKhau, int idVaiTro)
        {
            var user = await _context.NguoiDungs
                .FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap);

            if (user == null)
            {
                return new
                {
                    Success = false,
                    Message = "Tài khoản không tồn tại."
                };
            }
            if (!CommonService.VerifyPassword(matKhau,user.MatKhau))
            {
                return new
                {
                    Success = false,
                    Message = "Mật khẩu không chính xác."
                };
            }

            if (user.IdVaiTro != idVaiTro)
            {
                return new
                {
                    Success = false,
                    Message = "Bạn không có quyền truy cập với vai trò này."
                };
            }

            var vaiTro = await _context.VaiTros.FindAsync(idVaiTro);
            string tenVaiTro = vaiTro?.TenVaiTro ?? "Unknown";

            var userMap = _mapper.Map<NguoiDungMap>(user);
            var token = _jwtServices.GenerateToken(userMap, tenVaiTro);

            return new
            {
                Success = true,
                Message = "Đăng nhập thành công.",
                Token = token,
                Data = new
                {
                    Id = user.Id,
                    HoTen = user.HoTen,
                    Role = tenVaiTro
                }
            };
        }
    }
}
