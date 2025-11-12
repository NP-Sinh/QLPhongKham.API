using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class LichLamViec
{
    public int Id { get; set; }

    public string MaLich { get; set; } = null!;

    public int? IdBacSi { get; set; }

    public int? IdPhong { get; set; }

    public int? ThuTrongTuan { get; set; }

    public TimeOnly GioBatDau { get; set; }

    public TimeOnly GioKetThuc { get; set; }

    public int? SoBenhNhanToiDa { get; set; }

    public virtual BacSi? IdBacSiNavigation { get; set; }

    public virtual PhongKham? IdPhongNavigation { get; set; }
}
