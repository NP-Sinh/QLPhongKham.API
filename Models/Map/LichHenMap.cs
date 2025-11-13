namespace QLPhongKham.API.Models.Map
{
    public class LichHenMap
    {
        public int Id { get; set; }

        public string MaLichHen { get; set; } = null!;

        public int? IdBenhNhan { get; set; }

        public int? IdBacSi { get; set; }

        public int? IdPhong { get; set; }

        public DateTime NgayGioHen { get; set; }

        public string? TrangThai { get; set; }

        public string? TrieuChung { get; set; }

        public string? GhiChu { get; set; }

        public DateTime? NgayTao { get; set; }
    }
}
