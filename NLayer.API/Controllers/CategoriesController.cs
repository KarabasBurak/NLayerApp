using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
{
    
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // CategoryService'den çektik. GetSingleCategoryByIdWithProducts metodunu orada tanımladık ve burada çağırdık. Custom bir metodu orada CategoryService'de yazdık. Buraya da yazabilirdik
        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int categoryId)
        {
            var category=await _categoryService.GetSingleCategoryByIdWithProductsAsync(categoryId);
            return CreateActionResult(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var category=await _categoryService.GetAllAsync();
            var response= _mapper.Map<List<CategoryDto>>(category.ToList());
            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, response));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategories(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            var response = _mapper.Map<CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200, response));
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategories(CategoryDto categoryDto)
        {
            var category= await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));
            var response = _mapper.Map<CategoryDto>(category);
            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(201, response));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategories(CategoryDto categoryDto)
        {
            await _categoryService.UpdateAsync(_mapper.Map<Category>(categoryDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var product = await _categoryService.GetByIdAsync(id);
            await _categoryService.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        
    }
}
