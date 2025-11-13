namespace QLPhongKham.API.Models.Map
{
    public class ThuocMap
    {
        public int Id { get; set; }

        public string MaThuoc { get; set; } = null!;

        public string TenThuoc { get; set; } = null!;

        public string? HoatChat { get; set; }

        public string? DonVi { get; set; }

        public decimal DonGia { get; set; }

        public int? SoLuongTon { get; set; }

        public bool? DangHoatDong { get; set; }
    }
}
