using AutoMapper;
using Hubtel.Wallets.Application.DTOs.Types;
using Hubtel.Wallets.Domain.Models;

namespace Hubtel.Wallets.Application.Profiles.Types
{
    public class TypesProfile : Profile
    {
        public TypesProfile()
        {
            CreateMap<TblTtypeGet, TypeDto>().ReverseMap();
            CreateMap<TblTtype, CreateTypeDto>().ReverseMap();
            CreateMap<TblTtype, UpdateTypeDto>().ReverseMap();
        }
    }
}