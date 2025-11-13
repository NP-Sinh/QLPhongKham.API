using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services.ConvertDBToJsonServices;

namespace QLPhongKham.API.Services
{
    public interface IPhongKhamServices
    {
        Task<dynamic> getPhongKham();
        Task<dynamic> getPhongKhamId(int id);
        Task<dynamic> modify(PhongKhamMap phongKhamMap);
    }
    public class PhongKhamServices : IPhongKhamServices
    {
        private readonly PhongKhamDBContext _context;
        private readonly IMapper _mapper;
        private IConvertDBToJsonServices _servicesJson;
        public PhongKhamServices(PhongKhamDBContext context, IMapper mapper, IConvertDBToJsonServices services)
        {
            _context = context;
            _mapper = mapper;
            _servicesJson = services;
        }
        public async Task<dynamic> getPhongKham()
        {
            var query = await _context.PhongKhams
                .Select(pk => new
                {
                    Id = pk.Id,
                    MaPhong = pk.MaPhong,
                    TenPhong = pk.TenPhong,
                    LoaiPhong = pk.LoaiPhong,
                    Tang = pk.Tang
                })
                .ToListAsync();
            return query;
        }

        public async Task<dynamic> getPhongKhamId(int id)
        {
            var query = await _context.PhongKhams
                .Where(pk => pk.Id == id)
               .Select(pk => new
               {
                   Id = pk.Id,
                   MaPhong = pk.MaPhong,
                   TenPhong = pk.TenPhong,
                   LoaiPhong = pk.LoaiPhong,
                   Tang = pk.Tang
               })
               .FirstOrDefaultAsync();
            return query;
        }

        public async Task<dynamic> modify(PhongKhamMap phongKhamMap)
        {
            try
            {
                PhongKham model = _mapper.Map<PhongKham>(phongKhamMap);
                if (model.Id == 0)
                {
                    model.MaPhong = CommonService.LuuMaQL("PK", "PhongKham", _context);
                    await _context.PhongKhams.AddAsync(model);
                }
                else
                {
                    PhongKham update = await _context.PhongKhams.FindAsync(model.Id);
                    update.TenPhong = model.TenPhong;
                    update.LoaiPhong = model.LoaiPhong;
                    update.Tang = model.Tang;
                    _context.PhongKhams.Update(update);

                }
                await _context.SaveChangesAsync();
                await _servicesJson.convertPhongKhamToJson();
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
