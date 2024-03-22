using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            var product= await _productService.GetProductsWithCategory();
            return CreateActionResult(product);
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();

            var response = _mapper.Map<List<ProductDto>>(products.ToList());

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, response));
        }

        // [ServiceFilter(typeof(NotFoundFilter<>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var products = await _productService.GetByIdAsync(id);

            var response = _mapper.Map<ProductDto>(products);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, response));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var products = await _productService.AddAsync(_mapper.Map<Product>(productDto));

            var response = _mapper.Map<ProductDto>(products);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, response));
        }


        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            await _productService.UpdateAsync(_mapper.Map<Product>(productUpdateDto));

            // Geriye bir şey dönmeyeceğimiz için NoContentDto sınıfını kullandık. Başarılı lduğunu belirtmek için de 204 yazdık. Bu dönüş şekli başarılı olduğunda geçerlidir. İlerleyen zamanda başarısız olması gibi diğer StatusCode durumlarının da dönüş şekillerini yazacağız.
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));  
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product=await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); 

        }

    }
}
