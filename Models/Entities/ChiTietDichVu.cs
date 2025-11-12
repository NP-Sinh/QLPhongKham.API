using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class ChiTietDichVu
{
    public int Id { get; set; }

    public string MaChiTiet { get; set; } = null!;

    public int? IdPhieuKham { get; set; }

    public int? IdDichVu { get; set; }

    public int? SoLuong { get; set; }

    public decimal DonGia { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual DichVu? IdDichVuNavigation { get; set; }

    public virtual PhieuKhamBenh? IdPhieuKhamNavigation { get; set; }
}
