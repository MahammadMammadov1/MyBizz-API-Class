using Mamba_Class.DAL;
using Mamba_Class.Entites;
using Microsoft.EntityFrameworkCore;
using MyBizz.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBizz.Data.Repositories.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly AppDbContext _appDb;

        public GenericRepository(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        public DbSet<TEntity> Table => _appDb.Set<TEntity>();

        public async Task<int> CommitAsync()
        {
            return await _appDb.SaveChangesAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
        }

        public  void DeleteAsync(TEntity entity)
        {
             Table.Remove(entity);    
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null, params string[]? includes)
        {
            var query = GetQuery(includes);
            return expression is not null ? await query.Where(expression).ToListAsync() : await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>>? expression = null, params string[]? includes)
        {
            var query = GetQuery(includes);
            return expression is not null ? await query.Where(expression).FirstOrDefaultAsync() : await query.FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> GetQuery(string[]? includes)
        {
            var query = Table.AsQueryable();

            if (query is not null)
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
