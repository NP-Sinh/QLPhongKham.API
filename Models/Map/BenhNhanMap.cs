using QLPhongKham.API.Models.Entities;

namespace QLPhongKham.API.Models.Map
{
    public class BenhNhanMap
    {
        public int Id { get; set; }

        public string MaBenhNhan { get; set; } = null!;

        public string HoTen { get; set; } = null!;

        public DateOnly? NgaySinh { get; set; }

        public string? GioiTinh { get; set; }

        public string? SoDienThoai { get; set; }

        public string? DiaChi { get; set; }

        public string? Cmnd { get; set; }

        public string? NhomMau { get; set; }

        public string? DiUng { get; set; }

        public DateTime? NgayTao { get; set; }

    }
}
