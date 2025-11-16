namespace QLPhongKham.API.Models.Map
{
    public class ChiTietDonThuocMap
    {
        public int Id { get; set; }

        public string MaChiTiet { get; set; } = null!;

        public int? IdDonThuoc { get; set; }

        public int? IdThuoc { get; set; }

        public int SoLuong { get; set; }

        public string? LieuLuong { get; set; }

        public string? CachDung { get; set; }

        public int? SoNgayDung { get; set; }
    }
}
