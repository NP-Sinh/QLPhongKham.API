using System;
using System.Collections.Generic;

namespace QLPhongKham.API.Models.Entities;

public partial class RefreshToken
{
    public int Id { get; set; }

    public int? IdNguoiDung { get; set; }

    public string Token { get; set; } = null!;

    public string? JwtId { get; set; }

    public bool? IsUsed { get; set; }

    public bool? IsRevoked { get; set; }

    public DateTime ExpiryDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual NguoiDung? IdNguoiDungNavigation { get; set; }
}
