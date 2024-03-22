using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        // API Controller endpointinde yapacağımız işlemleri artık burada yapıyoruz. Çünkü Service Katmanı iş katmanımızdır. API de sadece buradaki metodu çağırıp action metoda yazacağız
        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            // _productRepository üzerinden GetProductWithCategory metodu ile DB'den direkt verileri çektik. product nesnesine atadık.
            var product = await _productRepository.GetProductsWithCategory();

            // product nesnesindeki veriler Product sınıfı tarafından listelenmişti. Burada Product'ı, ProductWithCategoryDto'ya dönüştürüp productDto nesnesine liste olarak atadık.
            var productDto = _mapper.Map<List<ProductWithCategoryDto>>(product);

            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200,productDto);

        }
    }
}
