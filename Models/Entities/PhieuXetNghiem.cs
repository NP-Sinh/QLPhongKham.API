using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class PhieuXetNghiem
{
    public int Id { get; set; }

    public string MaPhieuXn { get; set; } = null!;

    public int? IdPhieuKham { get; set; }

    public int? IdDichVu { get; set; }

    public DateTime? NgayChiDinh { get; set; }

    public int? IdBacSi { get; set; }

    public string? TrangThai { get; set; }

    public string? GhiChu { get; set; }

    public virtual BacSi? IdBacSiNavigation { get; set; }

    public virtual DichVu? IdDichVuNavigation { get; set; }

    public virtual PhieuKhamBenh? IdPhieuKhamNavigation { get; set; }

    public virtual ICollection<KetQuaXetNghiem> KetQuaXetNghiems { get; set; } = new List<KetQuaXetNghiem>();
}
