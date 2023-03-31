using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using RaceAndPerformance.Core.Data;
using RaceAndPerformance.Core.Entities;

namespace RaceAndPerformance.Dal.Repository.Contracts
{
    public class MssqlRepository<TEntity> : IMssqlRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DataContext _dataContext;
        private readonly DbSet<TEntity> _entitySet;

        public MssqlRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _entitySet = dataContext.Set<TEntity>();
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _entitySet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetAllWithRelatedDataAsync(CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _entitySet.AsNoTracking();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> GetAllByIdsAsync(List<long> ids, CancellationToken cancellationToken)
        {
            return _entitySet.AsNoTracking().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
        }

        public async Task<long> InsertAsync(TEntity obj, CancellationToken cancellationToken)
        {
            await _entitySet.AddAsync(obj);
            await _dataContext.SaveChangesAsync(cancellationToken);

            return obj.Id;
        }

        public async Task UpdateAsync(TEntity obj, CancellationToken cancellationToken)
        {
            _entitySet.Update(obj);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdateRangeAsync(List<TEntity> obj, CancellationToken cancellationToken)
        {
            _entitySet.UpdateRange(obj);
            return await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DeleteById(long entityId, CancellationToken cancellationToken)
        {
            var entityToDelete = await _entitySet.FindAsync(new object[] { entityId }, cancellationToken);

            if (entityToDelete != null)
            {
                _entitySet.Remove(entityToDelete);
                await _dataContext.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        public async Task<TEntity> GetByIdWithRelatedDataAsync(long entityId, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _entitySet.AsNoTracking().Where(x => x.Id == entityId);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}