using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services.ConvertDBToJsonServices;

namespace QLPhongKham.API.Services
{
    public interface IPhhieuKhamBenhServices
    {
        Task<dynamic> getPhieuKhamBenh();
        Task<dynamic> getPhieuKhamId(int id);
        Task<dynamic> modify(PhieuKhamBenhMap phieuKhamBenhMap);
    }
    public class PhieuKhamBenhServices : IPhhieuKhamBenhServices
    {
        private readonly PhongKhamDBContext _context;
        private readonly IMapper _mapper;
        private readonly IConvertDBToJsonServices _servicesJson;
        public PhieuKhamBenhServices(PhongKhamDBContext context, IMapper mapper, IConvertDBToJsonServices servicesJson)
        {
            _context = context;
            _mapper = mapper;
            _servicesJson = servicesJson;
        }
        public async Task<dynamic> getPhieuKhamBenh()
        {
            var query = await _context.PhieuKhamBenhs
                .Select(x => new
                {
                    id = x.Id,
                    maPhieuKham = x.MaPhieuKham,
                    idBenhNhan = x.IdBenhNhan,
                    idBacSi = x.IdBacSi,
                    idLichHen = x.IdLichHen,
                    ngayKham = x.NgayKham,
                    trieuChung = x.TrieuChung,
                    canNang = x.CanNang,
                    chieuCao = x.ChieuCao,
                    nhietDo = x.NhietDo,
                    huyetAp = x.HuyetAp,
                    nhipTim = x.NhipTim,
                    chanDoan = x.ChanDoan,
                    dieuTri = x.DieuTri,
                    loiDan = x.LoiDan,
                    ngayTaiKham = x.NgayTaiKham,
                    trangThai = x.TrangThai,
                    ghiChu = x.GhiChu,
                })
                .ToListAsync();
            return query;
        }

        public async Task<dynamic> getPhieuKhamId(int id)
        {
            var query = await _context.PhieuKhamBenhs
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    id = x.Id,
                    maPhieuKham = x.MaPhieuKham,
                    ngayKham = x.NgayKham,
                    trieuChung = x.TrieuChung,
                    canNang = x.CanNang,
                    chieuCao = x.ChieuCao,
                    nhietDo = x.NhietDo,
                    huyetAp = x.HuyetAp,
                    nhipTim = x.NhipTim,
                    chanDoan = x.ChanDoan,
                    dieuTri = x.DieuTri,
                    loiDan = x.LoiDan,
                    ngayTaiKham = x.NgayTaiKham,
                    trangThai = x.TrangThai,
                    ghiChu = x.GhiChu,
                    benhNhan = x.IdBenhNhanNavigation != null ? new
                    {
                        id = x.IdBenhNhanNavigation.Id,
                        maBenhNhan = x.IdBenhNhanNavigation.MaBenhNhan,
                        hoTen = x.IdBenhNhanNavigation.HoTen,
                        ngaySinh = x.IdBenhNhanNavigation.NgaySinh,
                        gioiTinh = x.IdBenhNhanNavigation.GioiTinh,
                        soDienThoai = x.IdBenhNhanNavigation.SoDienThoai,
                        diaChi = x.IdBenhNhanNavigation.DiaChi,
                        cmnd = x.IdBenhNhanNavigation.Cmnd,
                        nhomMau = x.IdBenhNhanNavigation.NhomMau,
                        diUng =  x.IdBenhNhanNavigation.DiUng

                    } : null,
                    lichHen = x.IdLichHenNavigation != null ? new
                    {
                        id = x.IdLichHenNavigation.Id,
                        maLichHen = x.IdLichHenNavigation.MaLichHen,
                        idPhong = x.IdLichHenNavigation.IdPhong,
                        maPhong = x.IdLichHenNavigation.IdPhongNavigation.MaPhong,
                        ngayGioHen = x.IdLichHenNavigation.NgayGioHen,
                        trieuChung = x.IdLichHenNavigation.TrieuChung,
                    } : null,
                    bacSi = x.IdBacSiNavigation != null ? new
                    {
                        id = x.IdBacSiNavigation.Id,
                        maBacSi = x.IdBacSiNavigation.MaBacSi,
                        hoTen = x.IdBacSiNavigation.HoTen,
                        ngaySinh = x.IdBacSiNavigation.NgaySinh,
                        gioiTinh = x.IdBacSiNavigation.GioiTinh,
                        idChuyenKhoa = x.IdBacSiNavigation.IdChuyenKhoa,
                        maChuyenKhoa = x.IdBacSiNavigation.IdChuyenKhoaNavigation.MaChuyenKhoa
                    } : null,

                })
                .FirstOrDefaultAsync();
            return query;
        }

        public async Task<dynamic> modify(PhieuKhamBenhMap phieuKhamBenhMap)
        {
            try
            {
                PhieuKhamBenh model = _mapper.Map<PhieuKhamBenh>(phieuKhamBenhMap);
                if(model.Id == 0)
                {
                    model.MaPhieuKham = CommonService.LuuMaQL("PKB", "PhieuKhamBenh", _context);
                    await _context.PhieuKhamBenhs.AddAsync(model);
                }
                else
                {
                    PhieuKhamBenh update = await _context.PhieuKhamBenhs.FindAsync(model.Id);
                    update.IdBenhNhan = model.IdBenhNhan;
                    update.IdBacSi = model.IdBacSi;
                    update.IdLichHen = model.IdLichHen;
                    update.NgayKham = model.NgayKham;
                    update.TrieuChung = model.TrieuChung;
                    update.CanNang = model.CanNang;
                    update.ChieuCao = model.ChieuCao;
                    update.NhietDo = model.NhietDo;
                    update.HuyetAp = model.HuyetAp;
                    update.NhipTim = model.NhipTim;
                    update.ChanDoan = model.ChanDoan;
                    update.DieuTri = model.DieuTri;
                    update.LoiDan = model.LoiDan;
                    update.NgayTaiKham = model.NgayTaiKham;
                    update.TrangThai = model.TrangThai;
                    update.GhiChu = model.GhiChu;
                    _context.PhieuKhamBenhs.Update(update);
                }
                await _context.SaveChangesAsync();
                await _servicesJson.convertPhieKhamBenhToJson();
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
                    StatusCodes = 500,
                    message = "Thất bại",
                    Inner = e.InnerException?.Message
                };
            }
        }
    }
}
