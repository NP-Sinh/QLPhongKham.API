using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services.ConvertDBToJsonServices;

namespace QLPhongKham.API.Services
{
    public interface INguoiDungServices
    {
        Task<dynamic> getNguoiDung();
        Task<dynamic> getNguoiDungId(int id);
        Task<dynamic> modify(NguoiDungMap nguoiDungMap);
    }
    public class NguoiDungServices : INguoiDungServices
    {
        private readonly PhongKhamDBContext _context;
        private readonly IMapper _mapper;
        private IConvertDBToJsonServices _servicesJson;
        public NguoiDungServices(PhongKhamDBContext context, IMapper mapper, IConvertDBToJsonServices services)
        {
            _context = context;
            _mapper = mapper;
            _servicesJson = services;
        }
        public async Task<dynamic> getNguoiDung()
        {
            var query = await _context.NguoiDungs
               .Select(x => new
               {
                   Id = x.Id,
                   MaNguoiDung = x.MaNguoiDung,
                   TenDangNhap = x.TenDangNhap,
                   MatKhau = x.MatKhau,
                   HoTen = x.HoTen,
                   SoDienThoai = x.SoDienThoai,
                   Email = x.Email,
                   IdVaiTro = x.IdVaiTro,
                   TenVaiTro = x.IdVaiTroNavigation.TenVaiTro,
                   DangHoatDong = x.DangHoatDong,
                   NgayTao = x.NgayTao

               })
               .ToListAsync();
            return query;
        }

        public async Task<dynamic> getNguoiDungId(int id)
        {
            var query = await _context.NguoiDungs
                .Where(x => x.Id == id)
              .Select(x => new
              {
                  Id = x.Id,
                  MaNguoiDung = x.MaNguoiDung,
                  TenDangNhap = x.TenDangNhap,
                  MatKhau = x.MatKhau,
                  HoTen = x.HoTen,
                  SoDienThoai = x.SoDienThoai,
                  Email = x.Email,
                  VaiTro = x.IdVaiTroNavigation != null ? new
                  {
                      Id = x.IdVaiTroNavigation.Id,
                      MaVaiTro = x.IdVaiTroNavigation.MaVaiTro,
                      TenVaiTro = x.IdVaiTroNavigation.TenVaiTro,
                  } : null,
                  DangHoatDong = x.DangHoatDong,
                  NgayTao = x.NgayTao

              })
              .FirstOrDefaultAsync();
            return query;
        }

        public async Task<dynamic> modify(NguoiDungMap nguoiDungMap)
        {
            try
            {
                NguoiDung model = _mapper.Map<NguoiDung>(nguoiDungMap);
                if(model.Id == 0)
                {
                    model.MaNguoiDung = CommonService.LuuMaQL("ND", "NguoiDung", _context);
                    model.MatKhau = CommonService.HashPassword(nguoiDungMap.MatKhau);
                    model.DangHoatDong = true;
                    model.NgayTao = DateTime.Now;
                    _context.NguoiDungs.Add(model);
                }
                else
                {
                    NguoiDung update = await _context.NguoiDungs.FindAsync(model.Id);
                    update.MatKhau = CommonService.HashPassword(model.MatKhau);
                    update.HoTen = model.HoTen;
                    update.SoDienThoai = model.SoDienThoai;
                    update.Email = model.Email;
                    update.IdVaiTro = model.IdVaiTro;
                    update.DangHoatDong = model.DangHoatDong;
                    _context.NguoiDungs.Update(update);
                }
                await _context.SaveChangesAsync();
                await _servicesJson.convertNguoiDungToJson();
                return new
                {
                    statusCode = 200,
                    message = "Thành công",
                    data = model
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    statusCode = 500,
                    message = "Thất bại",
                    Inner = ex.InnerException?.Message
                };
            }

        }
    }
}
