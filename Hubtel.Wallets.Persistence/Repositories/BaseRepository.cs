using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly HubtelWalletsDbContext _context;

        public BaseRepository(HubtelWalletsDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Add(FormattableString sqlQuery)
        {
            await AddOrUpdateOrDelete(sqlQuery);
            return (await _context.Set<TEntity>().ToListAsync()).LastOrDefault();
        }

        public async Task<int> Delete(FormattableString sqlQuery)
        {
            return await AddOrUpdateOrDelete(sqlQuery);
        }

        public async Task<int> Update(FormattableString sqlQuery)
        {
            return await AddOrUpdateOrDelete(sqlQuery);
        }

        public async Task<TEntity> Get(FormattableString sqlQuery)
        {
            return (await Fetch(sqlQuery)).FirstOrDefault();
        }

        public async Task<IReadOnlyList<TEntity>> GetAll(FormattableString sqlQuery)
        {
            return await Fetch(sqlQuery);
        }

        public async Task<TEntity> GetByAnyField(FormattableString sqlQuery)
        {
            return (await _context.Set<TEntity>().FromSqlInterpolated(sqlQuery).ToListAsync()).FirstOrDefault();
        }

        private async Task<int> AddOrUpdateOrDelete(FormattableString sqlQuery)
        {
            return await _context.Database.ExecuteSqlInterpolatedAsync(sqlQuery);
        }

        private async Task<IReadOnlyList<TEntity>> Fetch(FormattableString sqlQuery)
        {
            var result = await _context.Set<TEntity>().FromSqlInterpolated(sqlQuery).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return (await _context.Set<TEntity>().FirstOrDefaultAsync(predicate));
        }

        public async Task<bool> Exists(int id)
        {
            return (await _context.Set<TEntity>().FindAsync(id)) != null;
        }

        public async Task<int> GetCount(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().CountAsync(predicate);
            //return (await Fetch(sqlQuery)).Count(predicate);
        }
    }
}