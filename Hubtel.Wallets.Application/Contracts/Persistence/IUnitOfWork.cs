using Hubtel.Wallets.Application.Contracts.Identity;
using Hubtel.Wallets.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<object> DefaultObjects { get; }
        IBaseRepository<TblAsAccountScheme> AccountSchemes { get; }
        IBaseRepository<TblAsAccountSchemeGet> AccountSchemesGet { get; }
        IBaseRepository<TblTtype> Ttypes { get; }
        IBaseRepository<TblTtypeGet> TtypesGet { get; }
        IBaseRepository<VwTnsTypeAndSchemeGet> VwTnsTypeAndSchemes { get; }
        IBaseRepository<VwCreditAccountsForUser> VwCreditAccountsForUser { get; }
        IBaseRepository<VwUcaUserCreditAccount> AllCreditAccounts { get; }
        IBaseRepository<TblUcaUserCreditAccount> TblUcaUserCreditAccount { get; }
        IBaseRepository<VwUserRolesAndClaim> VwUserRolesAndClaims { get; }

        IAuthService ApplicationUser { get; }

        Task Save();
    }
}