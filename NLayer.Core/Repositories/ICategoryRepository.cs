using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        // id'sine göre bir kategori dönmesi için bu metodu oluşturdum. 
        Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId);

    }
}
