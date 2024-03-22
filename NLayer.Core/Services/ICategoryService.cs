using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface ICategoryService:IService<Category>
    {
        // Dto döneceğiz. Service interfacelerinde dtolar üzerinden dönüş yapılır.
        Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId);
    }
}
