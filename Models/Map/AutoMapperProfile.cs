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
        }
    }
}
