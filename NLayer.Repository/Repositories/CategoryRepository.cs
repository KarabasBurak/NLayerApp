using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            return await _context.Categories.Include(x => x.Products).Where(x => x.Id == categoryId).SingleOrDefaultAsync();
        }
    }
}


// categoryId'ye göre Product'ları Categoriylere dâhil ederek getir. SingleOrDefaultAsync metodu kullanırsak categoryId'den birden fazla aynı Id var ise hata döner yani SingleOrDefaultAsync metodu aynı Id'ye sahip verileri getirmez hatayı döndürür. Bundan dolayı SingleOrDefault kullanmak mantıklıdır. Ama FirstOrDefaeult kullanırsak aynı Id'ye sahip verilerden ilk bulduğunu getirir. Id, PrimaryKey olduğu için aynı Id'ye sahip birden fazla veri olmamalıdır.
