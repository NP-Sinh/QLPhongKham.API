namespace QLPhongKham.API.Models.Map
{
    public class LichLamViecMap
    {
        public int Id { get; set; }

        public string MaLich { get; set; } = null!;

        public int? IdBacSi { get; set; }

        public int? IdPhong { get; set; }

        public int? ThuTrongTuan { get; set; }

        public TimeOnly GioBatDau { get; set; }

        public TimeOnly GioKetThuc { get; set; }

        public int? SoBenhNhanToiDa { get; set; }
    }
}
