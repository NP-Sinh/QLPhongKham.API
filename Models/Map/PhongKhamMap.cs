namespace QLPhongKham.API.Models.Map
{
    public class PhongKhamMap
    {
        public int Id { get; set; }

        public string MaPhong { get; set; } = null!;

        public string TenPhong { get; set; } = null!;

        public string? LoaiPhong { get; set; }

        public int? Tang { get; set; }
    }
}
