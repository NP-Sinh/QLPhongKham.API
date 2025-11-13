namespace QLPhongKham.API.Models.Map
{
    public class NguoiDungMap
    {
        public int Id { get; set; }

        public string MaNguoiDung { get; set; } = null!;

        public string TenDangNhap { get; set; } = null!;

        public string MatKhau { get; set; } = null!;

        public string HoTen { get; set; } = null!;

        public string? SoDienThoai { get; set; }

        public string? Email { get; set; }

        public int? IdVaiTro { get; set; }

        public bool? DangHoatDong { get; set; }

        public DateTime? NgayTao { get; set; }
    }
}
