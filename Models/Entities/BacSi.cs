using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class BacSi
{
    public int Id { get; set; }

    public string MaBacSi { get; set; } = null!;

    public int? IdNguoiDung { get; set; }

    public string HoTen { get; set; } = null!;

    public DateOnly? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? SoDienThoai { get; set; }

    public int? IdChuyenKhoa { get; set; }

    public string? BangCap { get; set; }

    public bool? DangHoatDong { get; set; }

    public virtual ICollection<DonThuoc> DonThuocs { get; set; } = new List<DonThuoc>();

    public virtual ChuyenKhoa? IdChuyenKhoaNavigation { get; set; }

    public virtual NguoiDung? IdNguoiDungNavigation { get; set; }

    public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();

    public virtual ICollection<LichLamViec> LichLamViecs { get; set; } = new List<LichLamViec>();

    public virtual ICollection<PhieuKhamBenh> PhieuKhamBenhs { get; set; } = new List<PhieuKhamBenh>();

    public virtual ICollection<PhieuXetNghiem> PhieuXetNghiems { get; set; } = new List<PhieuXetNghiem>();
}
