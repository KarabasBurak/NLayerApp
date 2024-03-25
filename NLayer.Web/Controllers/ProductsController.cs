using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductAPIService _productAPIService;
        private readonly CategoryAPIService _categoryAPIService;

        public ProductsController(ProductAPIService productAPIService, CategoryAPIService categoryAPIService)
        {
            _productAPIService = productAPIService;
            _categoryAPIService = categoryAPIService;
        }

        public async Task<IActionResult> GetProductsWithCategoryAsync()
        {
            var product= await _productAPIService.GetProductsWithCategoryAsync();
            return View(product);
        }

        public async Task<IActionResult> SaveProductAsync(ProductDto productDto)
        {
            var products=await _productAPIService.SaveProductAsync(productDto);
            return View(products);
        }

        
    }
}
