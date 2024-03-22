using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CategoryWithProductsDto : CategoryDto
    {
        // İlgili category'ye bağlı Productları da <List>ProductDto şeklinde DB'den çekeceğiz.
        public List<ProductDto> Products { get; set; }
    }
}
