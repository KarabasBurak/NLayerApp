﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Caching
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;

            // Cash işlemlerini yaptık 
            if(!_memoryCache.TryGetValue(CacheProductKey, out _)) // TryGetValue; cach'te varsa true veya false dönecek. (CacheProductKey, out _) ; cach key'e sahip data var mı yok mu ?
            {
                _memoryCache.Set(CacheProductKey, _productRepository.GetAll().ToList());
            }

        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _productRepository.AddAsync(entity); // AddAsync ile DB'ye ekledik. Kaydetmedik, memory'ye aldık.
            await _unitOfWork.CommitAsync(); // CommitAsync ile DB'ye kaydettik.
            await CacheAllProductAsync(); // Aşağıda tüm productları cachlemesi için metod yazıldı ve CacheAllProductAsync metodu burada çağırıldı.
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _productRepository.AddRangeAsync(entities); // AddAsync ile DB'ye ekledik. Kaydetmedik, memory'ye aldık.
            await _unitOfWork.CommitAsync(); // CommitAsync ile DB'ye kaydettik.
            await CacheAllProductAsync(); // Aşağıda tüm productları cachlemesi için metod yazıldı ve CacheAllProductAsync metodu burada çağırıldı.
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            return await _productRepository.AnyAsync(expression);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            var product = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            return Task.FromResult(product);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x=>x.Id == id);
            if (product == null)
            {
                throw new ClientSideException($" There is no {id} and {typeof(Product).Name} ");
            }

            return Task.FromResult(product);
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var product = await _productRepository.GetProductsWithCategory();
            var productDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productDto);
        }

        public async Task RemoveAsync(Product entity)
        {
            _productRepository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
             _productRepository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _productRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            // Cach'te sorulama yaptığımız için _memoryCache üzerinden Get metodu ile çağıracağız
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable(); 
        }

        public async Task CacheAllProductAsync()
        {
            _memoryCache.Set(CacheProductKey, await _productRepository.GetAll().ToListAsync());
        }
    }
}
