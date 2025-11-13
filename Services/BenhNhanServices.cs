using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services.ConvertDBToJsonServices;

namespace QLPhongKham.API.Services
{
    public interface IBenhNhanServices
    {
        Task<dynamic> getBenhNhan();
        Task<dynamic> getBenhNhanId(int id);
        Task<dynamic> modify(BenhNhanMap benhNhanMap);
    }
    public class BenhNhanServices : IBenhNhanServices
    {
        private readonly PhongKhamDBContext _context;
        private readonly IMapper _mapper;
        private IConvertDBToJsonServices _servicesJson;
        public BenhNhanServices(PhongKhamDBContext context, IMapper mapper, IConvertDBToJsonServices services)
        {
            _context = context;
            _mapper = mapper;
            _servicesJson = services;
        }
        public async Task<dynamic> getBenhNhan()
        {
           var query = await _context.BenhNhans
                .Select(bn => new
                {
                    Id = bn.Id,
                    MaBenhNhan = bn.MaBenhNhan,
                    HoTen = bn.HoTen,
                    NgaySinh = bn.NgaySinh,
                    GioiTinh = bn.GioiTinh,
                    SoDienThoai = bn.SoDienThoai,
                    DiaChi = bn.DiaChi,
                    Cmnd = bn.Cmnd,
                    NhomMau = bn.NhomMau,
                    DiUng = bn.DiUng,
                    NgayTao = bn.NgayTao
                })
                .ToListAsync();
            return query;
        }

        public async Task<dynamic> getBenhNhanId(int id)
        {
            var query = await _context.BenhNhans
                .Where(bn => bn.Id == id)
                .Select(bn => new
                {
                    Id = bn.Id,
                    MaBenhNhan = bn.MaBenhNhan,
                    HoTen = bn.HoTen,
                    NgaySinh = bn.NgaySinh,
                    GioiTinh = bn.GioiTinh,
                    SoDienThoai = bn.SoDienThoai,
                    DiaChi = bn.DiaChi,
                    Cmnd = bn.Cmnd,
                    NhomMau = bn.NhomMau,
                    DiUng = bn.DiUng,
                    NgayTao = bn.NgayTao,
                })
                .FirstOrDefaultAsync();
            return query;
        }

        public async Task<dynamic> modify(BenhNhanMap benhNhanMap)
        {
            try
            {
                BenhNhan model = _mapper.Map<BenhNhan>(benhNhanMap);
                if (model.Id == 0)
                {
                    model.MaBenhNhan = CommonService.LuuMaQL("BN", "BenhNhan", _context);
                    model.NgayTao = DateTime.Now;
                    await _context.BenhNhans.AddAsync(model);
                }
                else
                {
                    var update = await _context.BenhNhans.FindAsync(model.Id);
                    update.HoTen = model.HoTen;
                    update.NgaySinh = model.NgaySinh;
                    update.GioiTinh = model.GioiTinh;
                    update.SoDienThoai = model.SoDienThoai;
                    update.DiaChi = model.DiaChi;
                    update.Cmnd = model.Cmnd;
                    update.NhomMau = model.NhomMau;
                    update.DiUng = model.DiUng;
                }
                await _context.SaveChangesAsync();
                await _servicesJson.convertBenhNhanToJson();
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
