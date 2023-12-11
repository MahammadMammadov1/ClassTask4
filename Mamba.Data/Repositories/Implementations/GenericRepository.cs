using Mamba.Core.Models;
using Mamba.Core.Repositories.Interfaces;
using Mamba.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Data.Repositories.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        private readonly AppDbContext _appDb;

        public GenericRepository(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        public DbSet<TEntity> Table =>_appDb.Set<TEntity>();

        public async Task<int> CommitAsync()
        {
            return await _appDb.SaveChangesAsync();
        }

        public  async Task CreateAsync(TEntity entity)
        {
            await _appDb.AddAsync(entity);
        }

        public void DeleteAsync(TEntity entity)
        {
            _appDb.Remove(entity);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null, params string[]? includes)
        {
            var query = GetQuery(includes);

            return expression is not null
                        ? await query.Where(expression).ToListAsync()
                        : await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>>? expression = null, params string[]? includes)
        {
            var query = GetQuery(includes);

            return expression is not null
                    ? await query.Where(expression).FirstOrDefaultAsync()
                    : await query.FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> GetQuery(string[] includes)
        {
            var query = Table.AsQueryable();
            if (includes is not null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }
    }
}
