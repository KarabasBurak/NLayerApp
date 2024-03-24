using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        // IService, veritabanına değişiklikleri yansıtacağımız sınıftır. Ayrıca Update ve Remove hatta diğer metotlarınn dönüş türü Task oldu (Async). IGenericRepository'de tanımlanan metodlara göre farklılık bulunmaktaıdr.

        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression); // Sorgulu listeleme yapmak için IQuaryable kullandık
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity); 
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
