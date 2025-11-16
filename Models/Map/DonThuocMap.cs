using QLPhongKham.API.Models.Entities;

namespace QLPhongKham.API.Models.Map
{
    public class DonThuocMap
    {
        public int Id { get; set; }

        public string MaDonThuoc { get; set; } = null!;

        public int? IdPhieuKham { get; set; }

        public int? IdBenhNhan { get; set; }

        public int? IdBacSi { get; set; }

        public DateTime? NgayKeDon { get; set; }

        public string? TrangThai { get; set; }

        public string? GhiChu { get; set; }
        public virtual ICollection<ChiTietDonThuoc> ChiTietDonThuocs { get; set; } = new List<ChiTietDonThuoc>();
    }
}
