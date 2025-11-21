using AutoMapper;
using QLPhongKham.API.Models.Entities;

namespace QLPhongKham.API.Models.Map
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VaiTro, VaiTroMap>().ReverseMap();
            CreateMap<BenhNhan, BenhNhanMap>().ReverseMap();
            CreateMap<ChuyenKhoa, ChuyenKhoaMap>().ReverseMap();
            CreateMap<BacSi, BacSiMap>().ReverseMap();
            CreateMap<NguoiDung, NguoiDungMap>().ReverseMap();
            CreateMap<PhongKham, PhongKhamMap>().ReverseMap();
            CreateMap<Thuoc, ThuocMap>().ReverseMap();
            CreateMap<LichHen, LichHenMap>().ReverseMap();
            CreateMap<PhieuKhamBenh, PhieuKhamBenhMap>().ReverseMap();
            CreateMap<DonThuoc, DonThuocMap>().ReverseMap();
            CreateMap<ChiTietDonThuoc, ChiTietDonThuocMap>().ReverseMap();
            CreateMap<LichLamViec, LichLamViecMap>().ReverseMap();

        }
    }
}
