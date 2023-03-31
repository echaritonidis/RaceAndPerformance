using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;

namespace RaceAndPerformance.Dal.Repository.Contracts
{
    public interface IMssqlRepository<TEntity>
    {
        public Task<long> InsertAsync(TEntity obj, CancellationToken cancellationToken);
        public Task UpdateAsync(TEntity obj, CancellationToken cancellationToken);
        public Task<int> UpdateRangeAsync(List<TEntity> obj, CancellationToken cancellationToken);
        public Task<bool> DeleteById(long entityId, CancellationToken cancellationToken);
        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        public Task<List<TEntity>> GetAllByIdsAsync(List<long> ids, CancellationToken cancellationToken);
        public Task<List<TEntity>> GetAllWithRelatedDataAsync(CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties);
        public Task<TEntity> GetByIdWithRelatedDataAsync(long entityId, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}