using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services.ConvertDBToJsonServices;

namespace QLPhongKham.API.Services
{
    public interface IChuyenKhoaServices
    {
        Task<dynamic> getChuyenKhoa();
        Task<dynamic> getChuyenKhoaId(int id);
        Task<dynamic> modify(ChuyenKhoaMap chuyenKhoaMap);
    }
    public class ChuyenKhoaServices : IChuyenKhoaServices
    {
        private readonly PhongKhamDBContext _context;
        private readonly IMapper _mapper;
        private readonly IConvertDBToJsonServices _convertDBToJsonServices;
        public ChuyenKhoaServices(PhongKhamDBContext context, IMapper mapper, IConvertDBToJsonServices convertDBToJsonServices)
        {
            _context = context;
            _mapper = mapper;
            _convertDBToJsonServices = convertDBToJsonServices;
        }
        public async Task<dynamic> getChuyenKhoa()
        {
            var query = await _context.ChuyenKhoas
                .Select(x => new
                {
                    Id = x.Id,
                    MaChuyenKhoa = x.MaChuyenKhoa,
                    TenChuyenKhoa = x.TenChuyenKhoa
                })
                .ToListAsync();
            return query;
        }

        public async Task<dynamic> getChuyenKhoaId(int id)
        {
            var query = await _context.ChuyenKhoas
                .Where(x => x.Id == id)
               .Select(x => new
               {
                   Id = x.Id,
                   MaChuyenKhoa = x.MaChuyenKhoa,
                   TenChuyenKhoa = x.TenChuyenKhoa
               })
               .FirstOrDefaultAsync();
            return query;
        }

        public async Task<dynamic> modify(ChuyenKhoaMap chuyenKhoaMap)
        {
            try
            {
                ChuyenKhoa model = _mapper.Map<ChuyenKhoa>(chuyenKhoaMap);
                if(model.Id == 0)
                {
                    model.MaChuyenKhoa = CommonService.LuuMaQL("CK", "ChuyenKhoa", _context);
                    await _context.ChuyenKhoas.AddAsync(model);
                }    
                else
                {
                    ChuyenKhoa update = await _context.ChuyenKhoas.FindAsync(model.Id);
                    update.TenChuyenKhoa = model.TenChuyenKhoa;
                    _context.ChuyenKhoas.Update(model);
                }
                await _context.SaveChangesAsync();
                await _convertDBToJsonServices.convertChuyenKhoaToJson();
                return new
                {
                    StatusCodes = 200,
                    message = "Thành công",
                    data = model
                };
            }
            catch(Exception e)
            {
                return new
                {
                    StatusCodes = 500,
                    message = "Thất bại"
                };
            }
        }
    }
}
