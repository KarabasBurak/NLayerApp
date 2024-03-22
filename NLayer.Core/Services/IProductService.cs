using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductService:IService<Product>
    {
        // Bu aslında bir dönüş türü. API Controllerda dönüş türünü belirliyorduk artık Service katmanında yapacağız bu işlemleri
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory(); 
    }
}
