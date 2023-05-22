using Hubtel.Wallets.Application.Contracts.Identity;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Domain.Models;
using Hubtel.Wallets.Persistence.Context;
using System;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HubtelWalletsDbContext _dbContext;
        private IBaseRepository<object> _obectRepository;
        private IBaseRepository<TblAsAccountScheme> _accountSchemesRepository;
        private IBaseRepository<TblAsAccountSchemeGet> _accountSchemesGetRepository;
        private IBaseRepository<TblTtype> _tTypesRepository;
        private IBaseRepository<TblTtypeGet> _tTypesGetRepository;
        private IBaseRepository<VwTnsTypeAndSchemeGet> _vwTnsTypeAndSchemesRepository;
        private IBaseRepository<VwUcaUserCreditAccount> _vwUcaUserCreditAccountRepository;
        private IBaseRepository<VwCreditAccountsForUser> _vwCreditAccountsForUserRepository;
        private IBaseRepository<TblUcaUserCreditAccount> _tblUcaUserCreditAccountRepository;
        private IBaseRepository<VwUserRolesAndClaim> _vwUserRolesAndClaimsRepository;
        private IAuthService _applicationUser;

        public UnitOfWork(
            HubtelWalletsDbContext context
            , IBaseRepository<TblAsAccountScheme> accountSchemesRepository
            , IBaseRepository<TblTtype> tTypesRepository
            , IBaseRepository<VwTnsTypeAndSchemeGet> vwTnsTypeAndSchemesRepository
            , IBaseRepository<TblAsAccountSchemeGet> accountSchemesGetRepository
            , IBaseRepository<TblTtypeGet> tTypesGetRepository
            , IBaseRepository<VwUcaUserCreditAccount> vwUcaUserCreditAccountRepository
            , IBaseRepository<VwCreditAccountsForUser> vwCreditAccountsForUserRepository
            , IBaseRepository<TblUcaUserCreditAccount> tblUcaUserCreditAccountRepository
            , IBaseRepository<VwUserRolesAndClaim> vwUserRolesAndClaimsRepository
            , IAuthService authService)
        {
            _dbContext = context;
            _accountSchemesRepository = accountSchemesRepository;
            _tTypesRepository = tTypesRepository;
            _vwTnsTypeAndSchemesRepository = vwTnsTypeAndSchemesRepository;
            _accountSchemesGetRepository = accountSchemesGetRepository;
            _tTypesGetRepository = tTypesGetRepository;
            _vwUcaUserCreditAccountRepository = vwUcaUserCreditAccountRepository;
            _vwCreditAccountsForUserRepository = vwCreditAccountsForUserRepository;
            _tblUcaUserCreditAccountRepository = tblUcaUserCreditAccountRepository;
            _vwUserRolesAndClaimsRepository = vwUserRolesAndClaimsRepository;
            _applicationUser = authService;
        }

        public IBaseRepository<object> DefaultObjects => _obectRepository ?? new BaseRepository<object>(_dbContext);

        public IBaseRepository<TblAsAccountScheme> AccountSchemes => _accountSchemesRepository ?? new BaseRepository<TblAsAccountScheme>(_dbContext);
        public IBaseRepository<TblTtype> Ttypes => _tTypesRepository ?? new BaseRepository<TblTtype>(_dbContext);

        public IBaseRepository<VwTnsTypeAndSchemeGet> VwTnsTypeAndSchemes => _vwTnsTypeAndSchemesRepository ?? new BaseRepository<VwTnsTypeAndSchemeGet>(_dbContext);

        public IBaseRepository<TblAsAccountSchemeGet> AccountSchemesGet => _accountSchemesGetRepository ?? new BaseRepository<TblAsAccountSchemeGet>(_dbContext);

        public IBaseRepository<TblTtypeGet> TtypesGet => _tTypesGetRepository ?? new BaseRepository<TblTtypeGet>(_dbContext);

        public IBaseRepository<VwCreditAccountsForUser> VwCreditAccountsForUser => _vwCreditAccountsForUserRepository ?? new BaseRepository<VwCreditAccountsForUser>(_dbContext);

        public IBaseRepository<VwUcaUserCreditAccount> AllCreditAccounts => _vwUcaUserCreditAccountRepository ?? new BaseRepository<VwUcaUserCreditAccount>(_dbContext);

        public IBaseRepository<TblUcaUserCreditAccount> TblUcaUserCreditAccount => _tblUcaUserCreditAccountRepository ?? new BaseRepository<TblUcaUserCreditAccount>(_dbContext);

        public IBaseRepository<VwUserRolesAndClaim> VwUserRolesAndClaims => _vwUserRolesAndClaimsRepository ?? new BaseRepository<VwUserRolesAndClaim>(_dbContext);

        public IAuthService ApplicationUser => _applicationUser;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}