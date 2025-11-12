using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class DonThuoc
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

    public virtual BacSi? IdBacSiNavigation { get; set; }

    public virtual BenhNhan? IdBenhNhanNavigation { get; set; }

    public virtual PhieuKhamBenh? IdPhieuKhamNavigation { get; set; }
}
