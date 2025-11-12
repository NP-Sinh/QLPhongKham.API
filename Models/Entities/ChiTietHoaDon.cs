using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class ChiTietHoaDon
{
    public int Id { get; set; }

    public string MaChiTiet { get; set; } = null!;

    public int? IdHoaDon { get; set; }

    public string? LoaiMuc { get; set; }

    public string? TenMuc { get; set; }

    public int? SoLuong { get; set; }

    public decimal DonGia { get; set; }

    public decimal ThanhTien { get; set; }

    public virtual HoaDon? IdHoaDonNavigation { get; set; }
}
