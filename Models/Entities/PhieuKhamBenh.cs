using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class PhieuKhamBenh
{
    public int Id { get; set; }

    public string MaPhieuKham { get; set; } = null!;

    public int? IdBenhNhan { get; set; }

    public int? IdBacSi { get; set; }

    public int? IdLichHen { get; set; }

    public DateTime? NgayKham { get; set; }

    public string? TrieuChung { get; set; }

    public decimal? CanNang { get; set; }

    public decimal? ChieuCao { get; set; }

    public decimal? NhietDo { get; set; }

    public string? HuyetAp { get; set; }

    public int? NhipTim { get; set; }

    public string ChanDoan { get; set; } = null!;

    public string? DieuTri { get; set; }

    public string? LoiDan { get; set; }

    public DateOnly? NgayTaiKham { get; set; }

    public string? TrangThai { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietDichVu> ChiTietDichVus { get; set; } = new List<ChiTietDichVu>();

    public virtual ICollection<DonThuoc> DonThuocs { get; set; } = new List<DonThuoc>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual BacSi? IdBacSiNavigation { get; set; }

    public virtual BenhNhan? IdBenhNhanNavigation { get; set; }

    public virtual LichHen? IdLichHenNavigation { get; set; }

    public virtual ICollection<PhieuXetNghiem> PhieuXetNghiems { get; set; } = new List<PhieuXetNghiem>();
}
