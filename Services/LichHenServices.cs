using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services.ConvertDBToJsonServices;

namespace QLPhongKham.API.Services
{
    public interface ILichHenServices
    {
        Task<dynamic> getLichHen();
        Task<dynamic> getLichHenId(int id);
        Task<dynamic> modify(LichHenMap lichHenMap);
    }
    public class LichHenServices : ILichHenServices
    {
        private readonly PhongKhamDBContext _context;
        private readonly IMapper _mapper;
        private IConvertDBToJsonServices _servicesJson;
        public LichHenServices(PhongKhamDBContext context, IMapper mapper, IConvertDBToJsonServices services)
        {
            _context = context;
            _mapper = mapper;
            _servicesJson = services;
        }
        public async Task<dynamic> getLichHen()
        {
            var query = await _context.LichHens
               .Select(x => new
               {
                   Id = x.Id,
                   MaLichHen = x.MaLichHen,
                   NgayGioHen = x.NgayGioHen,
                   TrangThai = x.TrangThai,
                   TrieuChung = x.TrieuChung,
                   GhiChu = x.GhiChu,
                   NgayTao = x.NgayTao,
                   BenhNhan = x.IdBenhNhanNavigation != null ? new
                   {
                       Id = x.IdBenhNhanNavigation.Id,
                       MaBenhNhan = x.IdBenhNhanNavigation.MaBenhNhan,
                   } : null,
                   BacSi = x.IdBacSiNavigation != null ? new
                   {
                       Id = x.IdBacSiNavigation.Id,
                       MaBacSi = x.IdBacSiNavigation.MaBacSi,
                   } : null,
                   Phong = x.IdPhongNavigation != null ? new
                   {
                       Id = x.IdPhongNavigation.Id,
                       MaPhong = x.IdPhongNavigation.MaPhong,
                   } : null,
               })
               .ToListAsync();
            return query;
        }

        public async Task<dynamic> getLichHenId(int id)
        {
            var query = await _context.LichHens
                .Where(q => q.Id == id)
               .Select(x => new
               {
                   Id = x.Id,
                   MaLichHen = x.MaLichHen,
                   NgayGioHen = x.NgayGioHen,
                   TrangThai = x.TrangThai,
                   TrieuChung = x.TrieuChung,
                   GhiChu = x.GhiChu,
                   NgayTao = x.NgayTao,
                   BenhNhan = x.IdBenhNhanNavigation != null ? new
                   {
                       Id = x.IdBenhNhanNavigation.Id,
                       MaBenhNhan = x.IdBenhNhanNavigation.MaBenhNhan,
                       HoTen = x.IdBenhNhanNavigation.HoTen,
                       NgaySinh = x.IdBenhNhanNavigation.NgaySinh,
                       GioiTinh = x.IdBenhNhanNavigation.GioiTinh,
                       SoDienThoai = x.IdBenhNhanNavigation.SoDienThoai,
                       DiaChi = x.IdBenhNhanNavigation.DiaChi,
                       CMND = x.IdBenhNhanNavigation.Cmnd,
                       NhomMau = x.IdBenhNhanNavigation.NhomMau,
                       DiUng = x.IdBenhNhanNavigation.DiUng,
                       NgayTao = x.IdBenhNhanNavigation.NgayTao
                   } : null,
                   BacSi = x.IdBacSiNavigation != null ? new
                   {
                       Id = x.IdBacSiNavigation.Id,
                       MaBacSi = x.IdBacSiNavigation.MaBacSi,
                       HoTen = x.IdBacSiNavigation.HoTen,
                       NgaySinh = x.IdBacSiNavigation.NgaySinh,
                       GioiTinh = x.IdBacSiNavigation.GioiTinh,
                       ChuyenKhoa = x.IdBacSiNavigation.IdChuyenKhoaNavigation.TenChuyenKhoa,

                   } : null,
                   Phong = x.IdPhongNavigation != null ? new
                   {
                       Id = x.IdPhongNavigation.Id,
                       MaPhong = x.IdPhongNavigation.MaPhong,
                       TenPhong = x.IdPhongNavigation.TenPhong,
                       LoaiPhong = x.IdPhongNavigation.LoaiPhong,
                       Tang = x.IdPhongNavigation.Tang
                   } : null,
               })
               .FirstOrDefaultAsync();
            return query;
        }

        public async Task<dynamic> modify(LichHenMap lichHenMap)
        {
            try
            {
                LichHen model = _mapper.Map<LichHen>(lichHenMap);
                if(model.Id == 0)
                {
                    model.MaLichHen = CommonService.LuuMaQL("LH", "LichHen", _context);
                    model.TrangThai = "Đã đặt";
                    model.NgayTao = DateTime.Now;
                    await _context.LichHens.AddAsync(model);
                }   
                else
                {
                    LichHen update = await _context.LichHens.FindAsync(model.Id);
                    update.NgayGioHen = model.NgayGioHen;
                    update.TrangThai = model.TrangThai;
                    update.TrieuChung = model.TrieuChung;
                    update.GhiChu = model.GhiChu;
                    update.IdBacSi = model.IdBacSi;
                    update.IdPhong = model.IdPhong;
                    update.IdBenhNhan = model.IdBenhNhan;
                    _context.LichHens.Update(update);
                }
                await _context.SaveChangesAsync();
                await _servicesJson.convertLichHenToJson();
                return new
                {
                    statusCode = 200,
                    message = "Thành công",
                    data = model
                };
            }
            catch (Exception e)
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
