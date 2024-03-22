using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        // Product entitysine özel metodun ismini ve dönüş şeklini Product sınıfındaki entityleri listeleyerek döndürmesini belirledik.
        Task<List<Product>> GetProductsWithCategory(); 
    }
}
