using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class NguoiDung
{
    public int Id { get; set; }

    public string MaNguoiDung { get; set; } = null!;

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public int? IdVaiTro { get; set; }

    public bool? DangHoatDong { get; set; }

    public DateTime? NgayTao { get; set; }

    public virtual ICollection<BacSi> BacSis { get; set; } = new List<BacSi>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual VaiTro? IdVaiTroNavigation { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
