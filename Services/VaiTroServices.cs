using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services.ConvertDBToJsonServices;

namespace QLPhongKham.API.Services
{
    public interface IVaiTroServices
    {
        Task<dynamic> getVaiTro();
        Task<dynamic> getVaiTroId(int id);
        Task<dynamic> modify(VaiTroMap vaiTroMap);
    }
    public class VaiTroServices : IVaiTroServices
    {
        private readonly PhongKhamDBContext _context;
        private readonly IMapper _mapper;
        private IConvertDBToJsonServices _servicesJson;
        public VaiTroServices(PhongKhamDBContext context, IMapper mapper, IConvertDBToJsonServices servicesJson)
        {
            _context = context;
            _mapper = mapper;
            _servicesJson = servicesJson;
        }
        public async Task<dynamic> getVaiTro()
        {
           var query = await _context.VaiTros
                .Select(x => new
                {
                    Id = x.Id,
                    MaVaiTro = x.MaVaiTro,
                    TenVaiTro = x.TenVaiTro
                })
                .ToListAsync();
            return query;
        }

        public async Task<dynamic> getVaiTroId(int id)
        {
            var query = await _context.VaiTros
               .Where(x => x.Id == id)
               .Select(x => new
               {
                   Id = x.Id,
                   MaVaiTro = x.MaVaiTro,
                   TenVaiTro = x.TenVaiTro
               })
               .FirstOrDefaultAsync();
            return query;
        }

        public async Task<dynamic> modify(VaiTroMap vaiTroMap)
        {
            try
            {
                VaiTro model = _mapper.Map<VaiTro>(vaiTroMap);
                if (model.Id == 0)
                {
                    model.MaVaiTro = CommonService.LuuMaQL("VT","VaiTro", _context);
                    await _context.VaiTros.AddAsync(model);
                }
                else
                {
                    var update = await _context.VaiTros.FindAsync(model.Id);
                    update.TenVaiTro = model.TenVaiTro;
                    _context.VaiTros.Update(update);
                }    
                await _context.SaveChangesAsync();
                await _servicesJson.convertVaiTroToJson();
                return new
                {
                    StatusCodes = 200,
                    Message = "Thành công",
                    data = model
                };

            }
            catch (Exception ex)
            {
                return new
                {
                    StatusCodes = 500,
                    Message = "Thất bại"
                };

            }
        }
    }
}
