using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QLPhongKham.API.Models.Entities;

namespace QLPhongKham.API.Services.ConvertDBToJsonServices
{
    public interface IConvertDBToJsonServices
    {
        Task convertVaiTroToJson();
        Task convertNguoiDungToJson();
        Task convertBenhNhanToJson();
        Task convertChuyenKhoaToJson();
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
    }
}
