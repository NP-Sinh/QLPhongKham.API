using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class DichVu
{
    public int Id { get; set; }

    public string MaDichVu { get; set; } = null!;

    public string TenDichVu { get; set; } = null!;

    public string? LoaiDichVu { get; set; }

    public int? IdChuyenKhoa { get; set; }

    public decimal DonGia { get; set; }

    public int? ThoiLuong { get; set; }

    public virtual ICollection<ChiTietDichVu> ChiTietDichVus { get; set; } = new List<ChiTietDichVu>();

    public virtual ChuyenKhoa? IdChuyenKhoaNavigation { get; set; }

    public virtual ICollection<PhieuXetNghiem> PhieuXetNghiems { get; set; } = new List<PhieuXetNghiem>();
}
