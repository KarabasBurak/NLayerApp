using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class ProductServiceWithNoCaching : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductServiceWithNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var product = await _productRepository.GetProductsWithCategory();       
            var productDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200,productDto);

        }
    }
}

/*
 1) API Controller endpointinde yapacağımız işlemleri artık burada yapıyoruz. Çünkü Service Katmanı iş katmanımızdır. API de sadece buradaki metodu çağırıp action metoda yazacağız
 2) _productRepository üzerinden GetProductWithCategory metodu ile DB'den direkt verileri çektik. product nesnesine atadık.
 3) product nesnesindeki veriler Product sınıfı tarafından listelenmişti. Burada Product'ı, ProductWithCategoryDto'ya dönüştürüp productDto nesnesine liste olarak atadık.
 */
