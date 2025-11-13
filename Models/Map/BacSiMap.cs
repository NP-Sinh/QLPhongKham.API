namespace QLPhongKham.API.Models.Map
{
    public class BacSiMap
    {
        public int Id { get; set; }

        public string MaBacSi { get; set; } = null!;

        public int? IdNguoiDung { get; set; }

        public string HoTen { get; set; } = null!;

        public DateOnly? NgaySinh { get; set; }

        public string? GioiTinh { get; set; }

        public string? SoDienThoai { get; set; }

        public int? IdChuyenKhoa { get; set; }

        public string? BangCap { get; set; }

        public bool? DangHoatDong { get; set; }
    }
}
