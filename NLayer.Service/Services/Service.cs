using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Service.Services
{
    // Service, API ile generic repository arasında bir köprüdür. Ayrıca savechange metodu da burada çağırılır. SaveChange metodundna sonra yapmak istenilen işlem kaydedilir.
    // Repository katmanı yani EF Core DbContext sınıfında Crud veya diğer operasyonlar direkt olrak gerçekleşmez. Service katmanında bu işlemleri gerçekleştiriyoruz. Bu işlemleri service katmanında yapıp EF Core DbContext ile veritabanını haberleştiriyoruz.
    // Core katmanında, IGenericRepository ve IService sınıflarında farklı farklı metodlar tanımlanmasının sebebi budur. 
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork; // SaveChange burada taımlandı

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;

        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
            
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasProduct= await _repository.GetByIdAsync(id);
            if (hasProduct == null)
            {
                throw new ClientSideException($" There is no {id} and {typeof(T).Name} ");  // Client tarafından bir hata olduğu için ClientSideException üzerinden hata kodu ve mesajını döndürdük
            }
            return hasProduct;
        }

        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);  // Remove ve Update metodunun EF Core db context'te asenkron özelliği yok. await olmadan db'de silinecek veriyi sileceğimize dair işretledik.
            await _unitOfWork.CommitAsync();  // İşaretlenen veri de burada kaydedildi.
            
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }

        
    }
}
