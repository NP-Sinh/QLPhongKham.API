using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLPhongKham.API.Models.Entities;

public partial class PhongKhamDBContext : DbContext
{
    public PhongKhamDBContext()
    {
    }

    public PhongKhamDBContext(DbContextOptions<PhongKhamDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BacSi> BacSis { get; set; }

    public virtual DbSet<BenhNhan> BenhNhans { get; set; }

    public virtual DbSet<ChiTietDichVu> ChiTietDichVus { get; set; }

    public virtual DbSet<ChiTietDonThuoc> ChiTietDonThuocs { get; set; }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<ChuyenKhoa> ChuyenKhoas { get; set; }

    public virtual DbSet<DichVu> DichVus { get; set; }

    public virtual DbSet<DonThuoc> DonThuocs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KetQuaXetNghiem> KetQuaXetNghiems { get; set; }

    public virtual DbSet<LichHen> LichHens { get; set; }

    public virtual DbSet<LichLamViec> LichLamViecs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<PhieuKhamBenh> PhieuKhamBenhs { get; set; }

    public virtual DbSet<PhieuXetNghiem> PhieuXetNghiems { get; set; }

    public virtual DbSet<PhongKham> PhongKhams { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Thuoc> Thuocs { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BacSi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BacSi__3214EC07EB5CCC67");

            entity.ToTable("BacSi");

            entity.HasIndex(e => e.MaBacSi, "UQ__BacSi__E022715F5041F6E6").IsUnique();

            entity.Property(e => e.BangCap).HasMaxLength(100);
            entity.Property(e => e.DangHoatDong).HasDefaultValue(true);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MaBacSi).HasMaxLength(10);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);

            entity.HasOne(d => d.IdChuyenKhoaNavigation).WithMany(p => p.BacSis)
                .HasForeignKey(d => d.IdChuyenKhoa)
                .HasConstraintName("FK__BacSi__IdChuyenK__59FA5E80");

            entity.HasOne(d => d.IdNguoiDungNavigation).WithMany(p => p.BacSis)
                .HasForeignKey(d => d.IdNguoiDung)
                .HasConstraintName("FK__BacSi__IdNguoiDu__59063A47");
        });

        modelBuilder.Entity<BenhNhan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BenhNhan__3214EC072EC57246");

            entity.ToTable("BenhNhan");

            entity.HasIndex(e => e.MaBenhNhan, "UQ__BenhNhan__22A8B331B819E776").IsUnique();

            entity.Property(e => e.Cmnd)
                .HasMaxLength(20)
                .HasColumnName("CMND");
            entity.Property(e => e.DiUng).HasMaxLength(500);
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MaBenhNhan).HasMaxLength(10);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NhomMau).HasMaxLength(5);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
        });

        modelBuilder.Entity<ChiTietDichVu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChiTietD__3214EC071267CE7F");

            entity.ToTable("ChiTietDichVu");

            entity.HasIndex(e => e.MaChiTiet, "UQ__ChiTietD__CDF0A1159519F3C0").IsUnique();

            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaChiTiet).HasMaxLength(10);
            entity.Property(e => e.SoLuong).HasDefaultValue(1);
            entity.Property(e => e.ThanhTien)
                .HasComputedColumnSql("([SoLuong]*[DonGia])", false)
                .HasColumnType("decimal(29, 2)");

            entity.HasOne(d => d.IdDichVuNavigation).WithMany(p => p.ChiTietDichVus)
                .HasForeignKey(d => d.IdDichVu)
                .HasConstraintName("FK__ChiTietDi__IdDic__787EE5A0");

            entity.HasOne(d => d.IdPhieuKhamNavigation).WithMany(p => p.ChiTietDichVus)
                .HasForeignKey(d => d.IdPhieuKham)
                .HasConstraintName("FK__ChiTietDi__IdPhi__778AC167");
        });

        modelBuilder.Entity<ChiTietDonThuoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChiTietD__3214EC072E5143DA");

            entity.ToTable("ChiTietDonThuoc");

            entity.HasIndex(e => e.MaChiTiet, "UQ__ChiTietD__CDF0A115D849A8B1").IsUnique();

            entity.Property(e => e.CachDung).HasMaxLength(200);
            entity.Property(e => e.LieuLuong).HasMaxLength(100);
            entity.Property(e => e.MaChiTiet).HasMaxLength(10);

            entity.HasOne(d => d.IdDonThuocNavigation).WithMany(p => p.ChiTietDonThuocs)
                .HasForeignKey(d => d.IdDonThuoc)
                .HasConstraintName("FK__ChiTietDo__IdDon__114A936A");

            entity.HasOne(d => d.IdThuocNavigation).WithMany(p => p.ChiTietDonThuocs)
                .HasForeignKey(d => d.IdThuoc)
                .HasConstraintName("FK__ChiTietDo__IdThu__123EB7A3");
        });

        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChiTietH__3214EC07B618D313");

            entity.ToTable("ChiTietHoaDon");

            entity.HasIndex(e => e.MaChiTiet, "UQ__ChiTietH__CDF0A115F6E64B7A").IsUnique();

            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LoaiMuc).HasMaxLength(50);
            entity.Property(e => e.MaChiTiet).HasMaxLength(10);
            entity.Property(e => e.SoLuong).HasDefaultValue(1);
            entity.Property(e => e.TenMuc).HasMaxLength(200);
            entity.Property(e => e.ThanhTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdHoaDonNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.IdHoaDon)
                .HasConstraintName("FK__ChiTietHo__IdHoa__1EA48E88");
        });

        modelBuilder.Entity<ChuyenKhoa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChuyenKh__3214EC0722340DC6");

            entity.ToTable("ChuyenKhoa");

            entity.HasIndex(e => e.MaChuyenKhoa, "UQ__ChuyenKh__CD0E428E7A978F3F").IsUnique();

            entity.HasIndex(e => e.TenChuyenKhoa, "UQ__ChuyenKh__E7F9B928B2CDBB46").IsUnique();

            entity.Property(e => e.MaChuyenKhoa).HasMaxLength(10);
            entity.Property(e => e.TenChuyenKhoa).HasMaxLength(100);
        });

        modelBuilder.Entity<DichVu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DichVu__3214EC071C8B8A2E");

            entity.ToTable("DichVu");

            entity.HasIndex(e => e.MaDichVu, "UQ__DichVu__C0E6DE8E35E8760F").IsUnique();

            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LoaiDichVu).HasMaxLength(50);
            entity.Property(e => e.MaDichVu).HasMaxLength(10);
            entity.Property(e => e.TenDichVu).HasMaxLength(200);

            entity.HasOne(d => d.IdChuyenKhoaNavigation).WithMany(p => p.DichVus)
                .HasForeignKey(d => d.IdChuyenKhoa)
                .HasConstraintName("FK__DichVu__IdChuyen__5070F446");
        });

        modelBuilder.Entity<DonThuoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DonThuoc__3214EC0730F17D3D");

            entity.ToTable("DonThuoc");

            entity.HasIndex(e => e.MaDonThuoc, "UQ__DonThuoc__3EF99EE0376554E5").IsUnique();

            entity.Property(e => e.GhiChu).HasMaxLength(500);
            entity.Property(e => e.MaDonThuoc).HasMaxLength(10);
            entity.Property(e => e.NgayKeDon)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Chờ cấp thuốc");

            entity.HasOne(d => d.IdBacSiNavigation).WithMany(p => p.DonThuocs)
                .HasForeignKey(d => d.IdBacSi)
                .HasConstraintName("FK__DonThuoc__IdBacS__0B91BA14");

            entity.HasOne(d => d.IdBenhNhanNavigation).WithMany(p => p.DonThuocs)
                .HasForeignKey(d => d.IdBenhNhan)
                .HasConstraintName("FK__DonThuoc__IdBenh__0A9D95DB");

            entity.HasOne(d => d.IdPhieuKhamNavigation).WithMany(p => p.DonThuocs)
                .HasForeignKey(d => d.IdPhieuKham)
                .HasConstraintName("FK__DonThuoc__IdPhie__09A971A2");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HoaDon__3214EC0797CF5CE0");

            entity.ToTable("HoaDon");

            entity.HasIndex(e => e.MaHoaDon, "UQ__HoaDon__835ED13AD182B9C0").IsUnique();

            entity.Property(e => e.GhiChu).HasMaxLength(500);
            entity.Property(e => e.GiamGia)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaHoaDon).HasMaxLength(10);
            entity.Property(e => e.NgayLapHoaDon)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PhuongThucThanhToan).HasMaxLength(50);
            entity.Property(e => e.ThanhToan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Đã thanh toán");

            entity.HasOne(d => d.IdBenhNhanNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.IdBenhNhan)
                .HasConstraintName("FK__HoaDon__IdBenhNh__160F4887");

            entity.HasOne(d => d.IdNguoiTaoNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.IdNguoiTao)
                .HasConstraintName("FK__HoaDon__IdNguoiT__1AD3FDA4");

            entity.HasOne(d => d.IdPhieuKhamNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.IdPhieuKham)
                .HasConstraintName("FK__HoaDon__IdPhieuK__17036CC0");
        });

        modelBuilder.Entity<KetQuaXetNghiem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KetQuaXe__3214EC07949B5E6C");

            entity.ToTable("KetQuaXetNghiem");

            entity.HasIndex(e => e.MaKetQua, "UQ__KetQuaXe__D5B3102B3A686713").IsUnique();

            entity.Property(e => e.ChiSoBinhThuong).HasMaxLength(100);
            entity.Property(e => e.DonVi).HasMaxLength(50);
            entity.Property(e => e.IdPhieuXn).HasColumnName("IdPhieuXN");
            entity.Property(e => e.KetQua).HasMaxLength(500);
            entity.Property(e => e.MaKetQua).HasMaxLength(10);
            entity.Property(e => e.NgayCoKetQua)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TenChiSo).HasMaxLength(200);

            entity.HasOne(d => d.IdPhieuXnNavigation).WithMany(p => p.KetQuaXetNghiems)
                .HasForeignKey(d => d.IdPhieuXn)
                .HasConstraintName("FK__KetQuaXet__IdPhi__04E4BC85");
        });

        modelBuilder.Entity<LichHen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LichHen__3214EC07F81E84F4");

            entity.ToTable("LichHen");

            entity.HasIndex(e => e.MaLichHen, "UQ__LichHen__150F264E4E277CC8").IsUnique();

            entity.Property(e => e.GhiChu).HasMaxLength(500);
            entity.Property(e => e.MaLichHen).HasMaxLength(10);
            entity.Property(e => e.NgayGioHen).HasColumnType("datetime");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Đã đặt");
            entity.Property(e => e.TrieuChung).HasMaxLength(500);

            entity.HasOne(d => d.IdBacSiNavigation).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.IdBacSi)
                .HasConstraintName("FK__LichHen__IdBacSi__693CA210");

            entity.HasOne(d => d.IdBenhNhanNavigation).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.IdBenhNhan)
                .HasConstraintName("FK__LichHen__IdBenhN__68487DD7");

            entity.HasOne(d => d.IdPhongNavigation).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.IdPhong)
                .HasConstraintName("FK__LichHen__IdPhong__6A30C649");
        });

        modelBuilder.Entity<LichLamViec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LichLamV__3214EC072C3581C3");

            entity.ToTable("LichLamViec");

            entity.HasIndex(e => e.MaLich, "UQ__LichLamV__728A9AE85176CF66").IsUnique();

            entity.Property(e => e.MaLich).HasMaxLength(10);
            entity.Property(e => e.SoBenhNhanToiDa).HasDefaultValue(20);

            entity.HasOne(d => d.IdBacSiNavigation).WithMany(p => p.LichLamViecs)
                .HasForeignKey(d => d.IdBacSi)
                .HasConstraintName("FK__LichLamVi__IdBac__5EBF139D");

            entity.HasOne(d => d.IdPhongNavigation).WithMany(p => p.LichLamViecs)
                .HasForeignKey(d => d.IdPhong)
                .HasConstraintName("FK__LichLamVi__IdPho__5FB337D6");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NguoiDun__3214EC07494B0779");

            entity.ToTable("NguoiDung");

            entity.HasIndex(e => e.TenDangNhap, "UQ__NguoiDun__55F68FC014CE754B").IsUnique();

            entity.HasIndex(e => e.MaNguoiDung, "UQ__NguoiDun__C539D763D052A0BF").IsUnique();

            entity.Property(e => e.DangHoatDong).HasDefaultValue(true);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MaNguoiDung).HasMaxLength(10);
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);

            entity.HasOne(d => d.IdVaiTroNavigation).WithMany(p => p.NguoiDungs)
                .HasForeignKey(d => d.IdVaiTro)
                .HasConstraintName("FK__NguoiDung__IdVai__3D5E1FD2");
        });

        modelBuilder.Entity<PhieuKhamBenh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhieuKha__3214EC07D81FB0C1");

            entity.ToTable("PhieuKhamBenh");

            entity.HasIndex(e => e.MaPhieuKham, "UQ__PhieuKha__FACA55DE8E728860").IsUnique();

            entity.Property(e => e.CanNang).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ChieuCao).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.HuyetAp).HasMaxLength(20);
            entity.Property(e => e.MaPhieuKham).HasMaxLength(10);
            entity.Property(e => e.NgayKham)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NhietDo).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Hoàn thành");

            entity.HasOne(d => d.IdBacSiNavigation).WithMany(p => p.PhieuKhamBenhs)
                .HasForeignKey(d => d.IdBacSi)
                .HasConstraintName("FK__PhieuKham__IdBac__70DDC3D8");

            entity.HasOne(d => d.IdBenhNhanNavigation).WithMany(p => p.PhieuKhamBenhs)
                .HasForeignKey(d => d.IdBenhNhan)
                .HasConstraintName("FK__PhieuKham__IdBen__6FE99F9F");

            entity.HasOne(d => d.IdLichHenNavigation).WithMany(p => p.PhieuKhamBenhs)
                .HasForeignKey(d => d.IdLichHen)
                .HasConstraintName("FK__PhieuKham__IdLic__71D1E811");
        });

        modelBuilder.Entity<PhieuXetNghiem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhieuXet__3214EC07766177E1");

            entity.ToTable("PhieuXetNghiem");

            entity.HasIndex(e => e.MaPhieuXn, "UQ__PhieuXet__880E56C36018F512").IsUnique();

            entity.Property(e => e.GhiChu).HasMaxLength(500);
            entity.Property(e => e.MaPhieuXn)
                .HasMaxLength(10)
                .HasColumnName("MaPhieuXN");
            entity.Property(e => e.NgayChiDinh)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Chờ xét nghiệm");

            entity.HasOne(d => d.IdBacSiNavigation).WithMany(p => p.PhieuXetNghiems)
                .HasForeignKey(d => d.IdBacSi)
                .HasConstraintName("FK__PhieuXetN__IdBac__00200768");

            entity.HasOne(d => d.IdDichVuNavigation).WithMany(p => p.PhieuXetNghiems)
                .HasForeignKey(d => d.IdDichVu)
                .HasConstraintName("FK__PhieuXetN__IdDic__7E37BEF6");

            entity.HasOne(d => d.IdPhieuKhamNavigation).WithMany(p => p.PhieuXetNghiems)
                .HasForeignKey(d => d.IdPhieuKham)
                .HasConstraintName("FK__PhieuXetN__IdPhi__7D439ABD");
        });

        modelBuilder.Entity<PhongKham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhongKha__3214EC07941BE21B");

            entity.ToTable("PhongKham");

            entity.HasIndex(e => e.MaPhong, "UQ__PhongKha__20BD5E5A0B8EA2D3").IsUnique();

            entity.HasIndex(e => e.TenPhong, "UQ__PhongKha__AE382B29E5BAD6C0").IsUnique();

            entity.Property(e => e.LoaiPhong).HasMaxLength(50);
            entity.Property(e => e.MaPhong).HasMaxLength(10);
            entity.Property(e => e.TenPhong).HasMaxLength(100);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC07939E1CD8");

            entity.ToTable("RefreshToken");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.IsRevoked).HasDefaultValue(false);
            entity.Property(e => e.IsUsed).HasDefaultValue(false);
            entity.Property(e => e.JwtId).HasMaxLength(100);
            entity.Property(e => e.Token).HasMaxLength(500);

            entity.HasOne(d => d.IdNguoiDungNavigation).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.IdNguoiDung)
                .HasConstraintName("FK__RefreshTo__IdNgu__4222D4EF");
        });

        modelBuilder.Entity<Thuoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Thuoc__3214EC079394ED83");

            entity.ToTable("Thuoc");

            entity.HasIndex(e => e.MaThuoc, "UQ__Thuoc__4BB1F6219C35968D").IsUnique();

            entity.Property(e => e.DangHoatDong).HasDefaultValue(true);
            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DonVi).HasMaxLength(50);
            entity.Property(e => e.HoatChat).HasMaxLength(200);
            entity.Property(e => e.MaThuoc).HasMaxLength(10);
            entity.Property(e => e.SoLuongTon).HasDefaultValue(0);
            entity.Property(e => e.TenThuoc).HasMaxLength(200);
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VaiTro__3214EC07A0E74463");

            entity.ToTable("VaiTro");

            entity.HasIndex(e => e.TenVaiTro, "UQ__VaiTro__1DA55814F8E27A4A").IsUnique();

            entity.HasIndex(e => e.MaVaiTro, "UQ__VaiTro__C24C41CE099D65B8").IsUnique();

            entity.Property(e => e.MaVaiTro).HasMaxLength(5);
            entity.Property(e => e.TenVaiTro).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
