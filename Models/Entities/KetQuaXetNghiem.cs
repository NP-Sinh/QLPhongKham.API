using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class KetQuaXetNghiem
{
    public int Id { get; set; }

    public string MaKetQua { get; set; } = null!;

    public int? IdPhieuXn { get; set; }

    public string TenChiSo { get; set; } = null!;

    public string? KetQua { get; set; }

    public string? DonVi { get; set; }

    public string? ChiSoBinhThuong { get; set; }

    public DateTime? NgayCoKetQua { get; set; }

    public virtual PhieuXetNghiem? IdPhieuXnNavigation { get; set; }
}
