using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class LichHen
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

    public virtual BacSi? IdBacSiNavigation { get; set; }

    public virtual BenhNhan? IdBenhNhanNavigation { get; set; }

    public virtual PhongKham? IdPhongNavigation { get; set; }

    public virtual ICollection<PhieuKhamBenh> PhieuKhamBenhs { get; set; } = new List<PhieuKhamBenh>();
}
