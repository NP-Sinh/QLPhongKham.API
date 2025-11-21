using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QLPhongKham.API.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QLPhongKham.API.Services.ConvertDBToJsonServices
{
    public interface IConvertDBToJsonServices
    {
        Task convertVaiTroToJson();
        Task convertNguoiDungToJson();
        Task convertBenhNhanToJson();
        Task convertChuyenKhoaToJson();
        Task convertBacSiToJson();
        Task convertPhongKhamToJson();
        Task convertThuocToJson();
        Task convertLichHenToJson();
        Task convertPhieKhamBenhToJson();
        Task convertDonThuocToJson();
        Task convertLichLamViecToJson();
        Task convertAllDBToJson();
    }
    public class ConvertDBToJsonServices : IConvertDBToJsonServices
    {
        private readonly PhongKhamDBContext _context;

        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly string _exportPath;
        public ConvertDBToJsonServices(PhongKhamDBContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _exportPath = Path.Combine(
                _hostingEnvironment.WebRootPath,
                "exports",
                "json"
            );
        }
        private async Task SaveToJsonFile<T>(T data, string fileName)
        {
            try
            {
                Directory.CreateDirectory(_exportPath);
                string json = JsonConvert.SerializeObject(
                    data,
                    Formatting.Indented
                );
                string filePath = Path.Combine(_exportPath, fileName);

                await File.WriteAllTextAsync(filePath, json);

                Console.WriteLine($"Đã export: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi export {fileName}: {ex.Message}");
                throw;
            }
        }
        public async Task convertAllDBToJson()
        {
            await convertVaiTroToJson();
        }
        public async Task convertVaiTroToJson()
        {
            var data = await _context.VaiTros
                .Select(x => new
                {
                    id = x.Id,
                    maVaiTro = x.MaVaiTro,
                    tenVaiTro = x.TenVaiTro,
                })
                .OrderBy(x => x.id)
                .ToListAsync();

            await SaveToJsonFile(data, "VaiTro.json");
        }
        public async Task convertNguoiDungToJson()
        {
            var data = await _context.NguoiDungs
                .Select(x => new
                {
                    id = x.Id,
                    maNguoiDung = x.MaNguoiDung,
                    tenDangNhap = x.TenDangNhap,
                    hoTen = x.HoTen,
                    soDienThoai = x.SoDienThoai,
                    email = x.Email,
                    VaiTro = x.IdVaiTroNavigation != null ? new
                    {
                        id = x.IdVaiTroNavigation.Id,
                        maVaiTro = x.IdVaiTroNavigation.MaVaiTro,
                        tenVaiTro = x.IdVaiTroNavigation.TenVaiTro,
                    } : null,
                    dangHoatDong = x.DangHoatDong,

                })
                .OrderBy(x => x.id)
                .ToListAsync();

            await SaveToJsonFile(data, "NguoiDung.json");
        }
        public async Task convertBenhNhanToJson()
        {
            var data = await _context.BenhNhans
                .Select(x => new
                {
                    id = x.Id,
                    maBenhNhan = x.MaBenhNhan,
                    hoTen = x.HoTen,
                    ngaySinh = x.NgaySinh,
                    gioiTinh = x.GioiTinh,
                    soDienThoai = x.SoDienThoai,
                    diaChi = x.DiaChi,
                    cmnd = x.Cmnd,
                    nhomMau = x.NhomMau,
                    diUng = x.DiUng,
                    ngayTao = x.NgayTao,
                })
                .OrderBy(x => x.id)
                .ToListAsync();

            await SaveToJsonFile(data, "BenhNhan.json");
        }
        public async Task convertChuyenKhoaToJson()
        {
            var data = await _context.ChuyenKhoas
                .Select(x => new
                {
                    id = x.Id,
                    maChuyenKhoa = x.MaChuyenKhoa,
                    tenChuyenKhoa = x.TenChuyenKhoa,
                })
                .OrderBy(x => x.id)
                .ToListAsync();

            await SaveToJsonFile(data, "ChuyenKhoa.json");
        }

        public async Task convertBacSiToJson()
        {
            var data = await _context.BacSis
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
                .OrderBy(x => x.Id)
                .ToListAsync();

            await SaveToJsonFile(data, "BacSi.json");
        }

        public async Task convertPhongKhamToJson()
        {
            var data = await _context.PhongKhams
                 .Select(x => new
                 {
                     Id = x.Id,
                     MaPhong = x.MaPhong,
                     TenPhong = x.TenPhong,
                     LoaiPhong = x.LoaiPhong,
                     Tang = x.Tang,
                 })
                .OrderBy(x => x.Id)
                .ToListAsync();

            await SaveToJsonFile(data, "PhongKham.json");
        }

        public async Task convertThuocToJson()
        {
            var data = await _context.Thuocs
                .Select(x => new
                {
                    Id = x.Id,
                    MaThuoc = x.MaThuoc,
                    TenThuoc = x.TenThuoc,
                    HoatChat = x.HoatChat,
                    DonVi = x.DonVi,
                    DonGia = x.DonGia,
                    SoLuongTon = x.SoLuongTon,
                    DangHoatDong = x.DangHoatDong,

                })
               .OrderBy(x => x.Id)
               .ToListAsync();

            await SaveToJsonFile(data, "Thuoc.json");
        }

        public async Task convertLichHenToJson()
        {
            var data = await _context.LichHens
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
                .OrderBy(x => x.Id)
                .ToListAsync();
            await SaveToJsonFile(data, "LichHen.json");
        }

        public async Task convertPhieKhamBenhToJson()
        {
            var data = await _context.PhieuKhamBenhs
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
                        diUng = x.IdBenhNhanNavigation.DiUng

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
                .OrderBy(x => x.id)
                .ToListAsync();
            await SaveToJsonFile(data, "PhieuKhamBenh.json");
        }

        public async Task convertDonThuocToJson()
        {
            var data = await _context.DonThuocs
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
              .OrderBy(x => x.id)
              .ToListAsync();
            await SaveToJsonFile(data, "DonThuoc.json");
        }

        public async Task convertLichLamViecToJson()
        {
            var data = await _context.LichLamViecs
              .Select(x => new
              {
                  id = x.Id,
                  thuTrongTuan = x.ThuTrongTuan,
                  gioBatDau = x.GioBatDau,
                  gioKetThuc = x.GioKetThuc,
                  soBNToiDa = x.SoBenhNhanToiDa,
                  bacSi = x.IdBacSiNavigation != null ? new
                  {
                      idBacSi = x.IdBacSi,
                      maBacSi = x.IdBacSiNavigation.MaBacSi,
                      hoTen = x.IdBacSiNavigation.HoTen,
                      chuyenKhoa = x.IdBacSiNavigation.IdChuyenKhoaNavigation.TenChuyenKhoa,
                  } : null,
                  phong = x.IdPhongNavigation != null ? new
                  {
                      idPhong = x.IdPhong,
                      maPhong = x.IdPhongNavigation.MaPhong,
                      tenPhong = x.IdPhongNavigation.TenPhong,
                  } : null
                  
              })
              .OrderBy(x => x.id)
              .ToListAsync();
            await SaveToJsonFile(data, "LichLamViec.json");
        }
    }
}
