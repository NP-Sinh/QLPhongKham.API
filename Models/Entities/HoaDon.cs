using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class HoaDon
{
    public int Id { get; set; }

    public string MaHoaDon { get; set; } = null!;

    public int? IdBenhNhan { get; set; }

    public int? IdPhieuKham { get; set; }

    public DateTime? NgayLapHoaDon { get; set; }

    public decimal TongTien { get; set; }

    public decimal? GiamGia { get; set; }

    public decimal ThanhToan { get; set; }

    public string? PhuongThucThanhToan { get; set; }

    public string? TrangThai { get; set; }

    public int? IdNguoiTao { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual BenhNhan? IdBenhNhanNavigation { get; set; }

    public virtual NguoiDung? IdNguoiTaoNavigation { get; set; }

    public virtual PhieuKhamBenh? IdPhieuKhamNavigation { get; set; }
}
