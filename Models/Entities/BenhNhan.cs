using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class BenhNhan
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

    public virtual ICollection<DonThuoc> DonThuocs { get; set; } = new List<DonThuoc>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();

    public virtual ICollection<PhieuKhamBenh> PhieuKhamBenhs { get; set; } = new List<PhieuKhamBenh>();
}
