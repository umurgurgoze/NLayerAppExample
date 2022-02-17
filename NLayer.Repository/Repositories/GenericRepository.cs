using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class // EfCore classlar ile çalışıyor.T'nin bir class olduğunu belirtmemiz gerekiyor.
    {
        protected readonly AppDbContext _context; // Farklı bir Entity ile ilgili farklı bir metota ihtiyacımız olursa, özel metot yazabilmemiz için 
                                                  // burayı protected olarak alıyoruz.Protected'a sadece miras alınan yerden erişilebilir.
        private readonly DbSet<T> _dbSet;         // Readonly tanımlama sebebimiz,bu değişkenleri ya bu esnada ya da constructorda değer atayabilmemiz için.
                                                  // Aşağıda değer atama hatası yapmamak için yani.
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

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbSet.AsNoTracking().AsQueryable(); // AsNoTracking yazmazsak tüm dataları memory'e alır ve durumlarını izler.Performans düşer.
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity) //Buraya kadar asenkron.Remove dediğimizde state deleted olarak işaretlenir.
        {
            _dbSet.Remove(entity);
            //_context.Entry(entity).State = EntityState.Deleted; ==> Bu şekilde de yazılabilir.
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
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
