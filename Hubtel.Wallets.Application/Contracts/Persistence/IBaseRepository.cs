using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Contracts.Persistence
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Add(FormattableString sqlQuery);

        Task<int> Update(FormattableString sqlQuery);

        Task<int> Delete(FormattableString sqlQuery);

        Task<IReadOnlyList<TEntity>> GetAll(FormattableString sqlQuery);

        Task<TEntity> Get(FormattableString sqlQuery);

        Task<int> GetCount(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate);

        Task<bool> Exists(int id);

        Task<TEntity> GetByAnyField(FormattableString sqlQuery);
    }
}