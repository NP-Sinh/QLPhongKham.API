using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class PhongKham
{
    public int Id { get; set; }

    public string MaPhong { get; set; } = null!;

    public string TenPhong { get; set; } = null!;

    public string? LoaiPhong { get; set; }

    public int? Tang { get; set; }

    public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();

    public virtual ICollection<LichLamViec> LichLamViecs { get; set; } = new List<LichLamViec>();
}
