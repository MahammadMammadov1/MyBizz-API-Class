using Mamba_Class.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBizz.Core.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        public DbSet<TEntity> Table {  get; }
        Task  CreateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
        Task <List<TEntity>> GetAllAsync(Expression<Func<TEntity , bool>>? expression = null , params string[]? includes );
        Task <TEntity> GetByIdAsync(Expression<Func<TEntity , bool>>? expression = null , params string[]? includes );
        Task<int> CommitAsync();

    }
}
