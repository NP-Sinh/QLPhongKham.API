using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services.ConvertDBToJsonServices;

namespace QLPhongKham.API.Services
{
    public interface IThuocServices
    {
        Task<dynamic> getThuoc();
        Task<dynamic> getThuocId(int id);
        Task<dynamic> modify(ThuocMap thuocMap);
    }
    public class ThuocServices : IThuocServices
    {
        private readonly PhongKhamDBContext _context;
        private readonly IMapper _mapper;
        private IConvertDBToJsonServices _servicesJson;
        public ThuocServices(PhongKhamDBContext context, IMapper mapper, IConvertDBToJsonServices services)
        {
            _context = context;
            _mapper = mapper;
            _servicesJson = services;
        }
        public async Task<dynamic> getThuoc()
        {
            var query = await _context.Thuocs
                .Select(x => new
                {
                    Id = x.Id,
                    MaThuoc = x.MaThuoc,
                    TenThuoc = x.TenThuoc,
                    HoatChat = x.HoatChat,
                    DonVi = x.DonVi,
                    DonGia = x.DonGia,
                    SoLuongTon = x.SoLuongTon,
                    DangHoatDong = x.DangHoatDong
                })
                .ToListAsync();
            return query;
        }

        public async Task<dynamic> getThuocId(int id)
        {
            var query = await _context.Thuocs
                .Where(x => x.Id == id)
               .Select(x => new
               {
                   Id = x.Id,
                   MaThuoc = x.MaThuoc,
                   TenThuoc = x.TenThuoc,
                   HoatChat = x.HoatChat,
                   DonVi = x.DonVi,
                   DonGia = x.DonGia,
                   SoLuongTon = x.SoLuongTon,
                   DangHoatDong = x.DangHoatDong
               })
               .FirstOrDefaultAsync();
            return query;
        }

        public async Task<dynamic> modify(ThuocMap thuocMap)
        {
            try
            {
                Thuoc model = _mapper.Map<Thuoc>(thuocMap);
                if (model.Id == 0)
                {
                    model.MaThuoc = CommonService.LuuMaQL("TH", "Thuoc",_context);
                    model.DangHoatDong = true;
                    await _context.Thuocs.AddAsync(model);
                }
                else
                {
                    Thuoc update = await _context.Thuocs.FindAsync(model.Id);
                    update.TenThuoc = model.TenThuoc;
                    update.HoatChat = model.HoatChat;
                    update.DonVi = model.DonVi;
                    update.DonGia = model.DonGia;
                    update.SoLuongTon = model.SoLuongTon;
                    update.DangHoatDong = model.DangHoatDong;
                    _context.Thuocs.Update(update);
                }
                await _context.SaveChangesAsync();
                await _servicesJson.convertThuocToJson();
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
