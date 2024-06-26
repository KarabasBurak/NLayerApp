﻿using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using System.Linq.Expressions;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            // AsQueryable dönmemiizn sebebi; verilerin tamamını çekerken şartlı (order by gibi) çekmek için Queryable döndük. Daha sonra ToList() diyeceğiz.
            // AsNoTracking; EF Core çekmiş olduğu verileri memory'e almasın. Track etmezse daha performanslı çalışsın. AsNoTracking kullanmazsak 1000 tane veriyi memory'e alır ve izler. 
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity); // Burada DB'deki veriyi silmiyoruz. Sileceğimiz veriyi işaretliyoruz daha sonra SaveChange metodunu çağırınca sileceğiz.
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities); // foreach ile datalarda geziyor. Silinecek verileri işaretliyor. Çoklu silme işlemlerinde kullanacağız
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
