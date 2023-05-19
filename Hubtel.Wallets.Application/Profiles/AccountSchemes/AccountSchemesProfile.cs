using AutoMapper;
using Hubtel.Wallets.Application.DTOs.AccountSchemes;
using Hubtel.Wallets.Domain.Models;

namespace Hubtel.Wallets.Application.Profiles.AccountSchemes
{
    public class AccountSchemesProfile : Profile
    {
        public AccountSchemesProfile()
        {
            CreateMap<TblAsAccountScheme, CreateAccountSchemeDto>().ReverseMap();
            CreateMap<TblAsAccountScheme, UpdateAccountSchemeDto>().ReverseMap();
            CreateMap<TblAsAccountSchemeGet, AccountSchemeGetDto>().ReverseMap();
            CreateMap<VwTnsTypeAndSchemeGet, VwTnsTypeAndSchemeGetDto>().ReverseMap();
        }
    }
}