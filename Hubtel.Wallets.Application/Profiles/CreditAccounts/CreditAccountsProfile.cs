using AutoMapper;
using Hubtel.Wallets.Application.DTOs.CreditAccounts;
using Hubtel.Wallets.Domain.Models;

namespace Hubtel.Wallets.Application.Profiles.CreditAccounts
{
    public class CreditAccountsProfile : Profile
    {
        public CreditAccountsProfile()
        {
            CreateMap<TblUcaUserCreditAccount, CreateCreditAccountDto>().ReverseMap();
            CreateMap<TblUcaUserCreditAccount, UpdateCreditAccountDto>().ReverseMap();
            CreateMap<VwCreditAccountsForUser, AllCreditAccountsForUserDto>().ReverseMap();
            CreateMap<VwUcaUserCreditAccount, AllCreditAccountsDto>().ReverseMap();
        }
    }
}