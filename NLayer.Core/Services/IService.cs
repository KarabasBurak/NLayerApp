using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        // IService veritabanına değişiklikleri yansıtacağımız için SaveChangeAsync metodunu kullanacağımız için Update ve Remove hatta diğer metotlarınn dönüş türü Task oldu (Async)

        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression); // Sorgulu listeleme yapmak için IQuaryable kullandık
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity); 
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(T entity);
    }
}
