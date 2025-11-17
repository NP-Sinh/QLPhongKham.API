using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services.ConvertDBToJsonServices;

namespace QLPhongKham.API.Services
{
    public interface IBacSiServices
    {
        Task<dynamic> getBacSi();
        Task<dynamic> getBacSiId(int id);
        Task<dynamic> modify(BacSiMap bacSiMap);
    }
    public class BacSiServices : IBacSiServices
    {
        private readonly PhongKhamDBContext _context;
        private readonly IMapper _mapper;
        private IConvertDBToJsonServices _servicesJson;
        public BacSiServices(PhongKhamDBContext context, IMapper mapper, IConvertDBToJsonServices servicesJson)
        {
            _context = context;
            _mapper = mapper;
            _servicesJson = servicesJson;
        }
        public async Task<dynamic> getBacSi()
        {
            var query = await _context.BacSis
                .Select(bs => new
                {
                    Id = bs.Id,
                    MaBacSi = bs.MaBacSi,
                    HoTen = bs.HoTen,
                    NgaySinh = bs.NgaySinh,
                    GioiTinh = bs.GioiTinh,
                    SoDienThoai = bs.SoDienThoai,
                    BangCap = bs.BangCap,
                    DangHoatDong = bs.DangHoatDong,
                    ChuyenKhoa = bs.IdChuyenKhoaNavigation != null ? new
                    {
                        IdChuyenKhoa = bs.IdChuyenKhoa,
                        MaChuyenKhoa = bs.IdChuyenKhoaNavigation.MaChuyenKhoa,
                        TenChuyenKhoa = bs.IdChuyenKhoaNavigation.TenChuyenKhoa,
                    } : null,
                    NguoiDung = bs.IdNguoiDungNavigation != null ? new
                    {
                        IdChuyenKhoa = bs.IdChuyenKhoa,
                        MaNguoiDung = bs.IdNguoiDungNavigation.MaNguoiDung,
                        TenDangNhap = bs.IdNguoiDungNavigation.TenDangNhap,
                    } : null
                })
                .ToListAsync();
            return query;
        }

        public async Task<dynamic> getBacSiId(int id)
        {
            var query = await _context.BacSis
                .Where(bs => bs.Id == id)
               .Select(bs => new
               {
                   Id = bs.Id,
                   MaBacSi = bs.MaBacSi,
                   IdNguoiDung = bs.IdNguoiDung,
                   HoTen = bs.HoTen,
                   NgaySinh = bs.NgaySinh,
                   GioiTinh = bs.GioiTinh,
                   SoDienThoai = bs.SoDienThoai,
                   IdChuyenKhoa = bs.IdChuyenKhoa,
                   BangCap = bs.BangCap,
                   DangHoatDong = bs.DangHoatDong,
                   ChuyenKhoa = bs.IdChuyenKhoaNavigation != null ? new
                   {
                       Id = bs.IdChuyenKhoaNavigation.Id,
                       MaChuyenKhoa = bs.IdChuyenKhoaNavigation.MaChuyenKhoa,
                       TenChuyenKhoa = bs.IdChuyenKhoaNavigation.TenChuyenKhoa,
                   } : null,
                   NguoiDung = bs.IdNguoiDungNavigation != null ? new
                   {
                       Id = bs.IdNguoiDungNavigation.Id,
                       MaNguoiDung = bs.IdNguoiDungNavigation.MaNguoiDung,
                       TenDangNhap = bs.IdNguoiDungNavigation.TenDangNhap,
                   } : null
               })
               .FirstOrDefaultAsync();
            return query;
        }

        public async Task<dynamic> modify(BacSiMap bacSiMap)
        {
            try
            {
                BacSi model = _mapper.Map<BacSi>(bacSiMap);
                if (model.Id == 0)
                {
                    model.MaBacSi = CommonService.LuuMaQL("BS","BacSi", _context);
                    model.DangHoatDong = true;
                    await _context.BacSis.AddAsync(model);
                }
                else
                {
                    BacSi update = await _context.BacSis.FindAsync(model.Id);
                    update.HoTen = model.HoTen;
                    update.NgaySinh = model.NgaySinh;
                    update.GioiTinh = model.GioiTinh;
                    update.SoDienThoai = model.SoDienThoai;
                    update.IdChuyenKhoa = model.IdChuyenKhoa;
                    update.BangCap = model.BangCap;
                    update.DangHoatDong = model.DangHoatDong;

                    _context.BacSis.Update(update);
                }
                await _context.SaveChangesAsync();
                await _servicesJson.convertBacSiToJson();
                return new
                {
                    statusCode = 200,
                    message = "Thành công",
                    data = model
                };
            }
            catch(Exception e)
            {
                return new
                {
                    statusCode = 500,
                    message = "Thất bại",
                    Inner = e.InnerException?.Message
                };
            }
        }
    }
}
