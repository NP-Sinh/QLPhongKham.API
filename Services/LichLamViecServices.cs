
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services.ConvertDBToJsonServices;

namespace QLPhongKham.API.Services
{
    public interface ILichLamViecServices
    {
        Task<dynamic> getLichLamViec();
        Task<dynamic> getLichLamViecId(int id);
        Task<dynamic> modify(LichLamViecMap lichLamViecMap);

    }
    public class LichLamViecServices : ILichLamViecServices
    {

        private PhongKhamDBContext _context;
        private IMapper _mapper;
        private IConvertDBToJsonServices _servicesJson;
        public LichLamViecServices(PhongKhamDBContext context, IMapper mapper, IConvertDBToJsonServices servicesJson)
        {
            _context = context;
            _mapper = mapper;
            _servicesJson = servicesJson;
        }

        public async Task<dynamic> getLichLamViec()
        {
            return await _context.LichLamViecs.ToListAsync();
        }

        public async Task<dynamic> getLichLamViecId(int id)
        {
            return await _context.LichLamViecs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<dynamic> modify(LichLamViecMap lichLamViecMap)
        {
            try
            {
                LichLamViec model = _mapper.Map<LichLamViec>(lichLamViecMap);
                if (model.Id == 0)
                {
                    model.MaLich = CommonService.LuuMaQL("LLV", "LichLamViec", _context);
                    await _context.LichLamViecs.AddAsync(model);
                }
                else
                {
                    LichLamViec update = await _context.LichLamViecs.FindAsync(model.Id);
                    update.IdBacSi = model.IdBacSi;
                    update.IdPhong = model.IdPhong;
                    update.ThuTrongTuan = model.ThuTrongTuan;
                    update.GioBatDau = model.GioBatDau;
                    update.GioKetThuc = model.GioKetThuc;
                    update.SoBenhNhanToiDa = model.SoBenhNhanToiDa;
                    _context.LichLamViecs.Update(update);
                }    
                await _context.SaveChangesAsync();
                await _servicesJson.convertLichLamViecToJson();
                return new
                {
                    StatusCodes = 200,
                    message = "Thành công",
                    data = model
                };
            }
            catch (Exception ex) {
                return new
                {
                    StatusCodes = 500,
                    message = "Thất bại",
                };
            }
        }
    }
}
