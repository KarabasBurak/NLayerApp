using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
        // IProductRepository'de belirlediğimiz metodun burada kodlamasını yapıyorum. Ama DB ile olan bağlantısını(haberleşmesini) yapmak için kodlamasını yapıyorum.
        public async Task<List<Product>> GetProductsWithCategory()
        {
            // Include metodu ile Eager Loading yaptık. Yani product tablosundan veri çekerken AYNI ANDA kategorisini de çektim. Bu olay Eager Loading
            // Eğer product tablosundan bir veri çekerken DAHA SONRA kategorisini de çekersek Lazy Loading olur.
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
