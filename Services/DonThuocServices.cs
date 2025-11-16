using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Controllers;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services.ConvertDBToJsonServices;

namespace QLPhongKham.API.Services
{
    public interface IDonThuocServices
    {
        Task<dynamic> getDonThuoc();
        Task<dynamic> getDonThuocId(int id);
        Task<dynamic> createDonThuoc(DonThuocMap donThuocMap, List<ChiTietDonThuocMap> chiTietDonThuocMaps);
        Task<dynamic> deleteDonThuoc(int id);
        Task<dynamic> deleteCTDonThuoc(int id);
    }
    public class DonThuocServices : IDonThuocServices
    {
        private PhongKhamDBContext _context;
        private IMapper _mapper;
        private IConvertDBToJsonServices _servicesJson;
        public DonThuocServices(PhongKhamDBContext context, IMapper mapper, IConvertDBToJsonServices servicesJson)
        {
            _context = context;
            _mapper = mapper;
            _servicesJson = servicesJson;
        }
        public async Task<dynamic> getDonThuoc()
        {
            var query = await _context.DonThuocs
                .Select(x => new
                {
                    id = x.Id,
                    maDonThuoc = x.MaDonThuoc,
                    idPhieuKham = x.IdPhieuKham,
                    idBenhNhan = x.IdBenhNhan,
                    idBacSi = x.IdBacSi,
                    ngayKeDon = x.NgayKeDon,
                    trangThai = x.TrangThai,
                    ghiChu = x.GhiChu,
                })
                .ToListAsync();
            return query;
        }
        public async Task<dynamic> getDonThuocId(int id)
        {
            var query = await _context.DonThuocs
                .Where(x => x.Id == id)
               .Select(x => new
               {
                   id = x.Id,
                   maDonThuoc = x.MaDonThuoc,
                   idPhieuKham = x.IdPhieuKham,
                   idBenhNhan = x.IdBenhNhan,
                   idBacSi = x.IdBacSi,
                   ngayKeDon = x.NgayKeDon,
                   trangThai = x.TrangThai,
                   ghiChu = x.GhiChu,
                   ChiTietDonThuocs = x.ChiTietDonThuocs.Select(ct => new
                   {
                       id = ct.Id,
                       maChiTiet = ct.MaChiTiet,
                       idDonThuoc = ct.IdDonThuoc,
                       idThuoc = ct.IdThuoc,
                       soLuong = ct.SoLuong,
                       lieuLuong = ct.LieuLuong,
                       cachDung = ct.CachDung,
                       soNgayDung = ct.SoNgayDung
                   })
               })
               .FirstOrDefaultAsync();
            return query;
        }
        public async Task<dynamic> createDonThuoc(DonThuocMap donThuocMap, List<ChiTietDonThuocMap> chiTietDonThuocMaps)
        {
            var tran = _context.Database.BeginTransaction();
            try
            {
                DonThuoc master = _mapper.Map<DonThuoc>(donThuocMap);
                List<ChiTietDonThuoc> detail = _mapper.Map<List<ChiTietDonThuoc>>(chiTietDonThuocMaps);

                if(master.Id == 0)
                {
                    master.MaDonThuoc = CommonService.LuuMaQL("DT", "DonThuoc", _context);
                    await _context.DonThuocs.AddAsync(master);
                    await _context.SaveChangesAsync();

                    foreach(ChiTietDonThuoc ct in detail)
                    {
                        ct.IdDonThuoc = master.Id;
                        ct.MaChiTiet = CommonService.LuuMaQL("CTDT", "ChiTietDonThuoc", _context);
                    }
                    await _context.ChiTietDonThuocs.AddRangeAsync(detail);
                }
                else
                {
                    DonThuoc dt = await _context.DonThuocs.FindAsync(master.Id);
                    dt.IdPhieuKham = master.IdPhieuKham;
                    dt.IdBenhNhan = master.IdBenhNhan;
                    dt.IdBacSi = master.IdBacSi;
                    dt.NgayKeDon = master.NgayKeDon;
                    dt.TrangThai = master.TrangThai;
                    dt.GhiChu = master.GhiChu;
                    _context.DonThuocs.Update(dt);

                    var ctdt = _context.ChiTietDonThuocs.Where(x => x.IdDonThuoc == master.Id);
                    _context.ChiTietDonThuocs.RemoveRange(ctdt);
                    foreach (ChiTietDonThuoc ct in detail)
                    {
                        ct.Id = 0;
                        ct.IdDonThuoc = master.Id;
                        ct.MaChiTiet = CommonService.LuuMaQL("CTDT", "ChiTietDonThuoc", _context);
                    }
                    await _context.ChiTietDonThuocs.AddRangeAsync(detail);

                }
                await _context.SaveChangesAsync();
                tran.Commit();
                await _servicesJson.convertDonThuocToJson();
                return new
                {
                    statusCode = 200,
                    message = "Thành công",
                    Master = master,
                    Detail = detail,
                };
            }
            catch (Exception e)
            {
                tran.Rollback();
                return new
                {
                    statusCode = 500,
                    message = "Thất bại",
                    Inner = e.InnerException?.Message
                };
            }
        }
        public async Task<dynamic> deleteDonThuoc(int id)
        {
            try
            {
                var donThuoc = await _context.DonThuocs
                    .Include(x => x.ChiTietDonThuocs)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (donThuoc.ChiTietDonThuocs.Any())
                {
                    _context.ChiTietDonThuocs.RemoveRange(donThuoc.ChiTietDonThuocs);
                }

                _context.DonThuocs.Remove(donThuoc);

                await _context.SaveChangesAsync();

                return new
                {
                    statusCode = 200,
                    message = "Thành công",
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
        public async Task<dynamic> deleteCTDonThuoc(int id)
        {
            try
            {
                var detail = await _context.ChiTietDonThuocs.FindAsync(id);

                _context.ChiTietDonThuocs.Remove(detail);
                await _context.SaveChangesAsync();

                return new
                {
                    statusCode = 200,
                    message = "Thành công",
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
