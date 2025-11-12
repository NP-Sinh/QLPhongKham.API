using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class ChuyenKhoa
{
    public int Id { get; set; }

    public string MaChuyenKhoa { get; set; } = null!;

    public string TenChuyenKhoa { get; set; } = null!;

    public virtual ICollection<BacSi> BacSis { get; set; } = new List<BacSi>();

    public virtual ICollection<DichVu> DichVus { get; set; } = new List<DichVu>();
}
