using FurnitureShop.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected FurnitureShopContext _context;

        public GenericRepository(FurnitureShopContext context)
        {
            _context = context;
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ? _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().Where(expression);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
