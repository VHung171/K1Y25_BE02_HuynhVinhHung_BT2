using AutoMapper;
using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Request;
using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Response;
using K1Y25_BE02_HuynhVinhHung_BT2.Models;

namespace K1Y25_BE02_HuynhVinhHung_BT2.Mapper
{
    public class RoleMapper: Profile
    {
        public RoleMapper()
        {
            CreateMap<Role, RoleResponse>();
            CreateMap<RoleRequest, Role>();
        }
    }
}
