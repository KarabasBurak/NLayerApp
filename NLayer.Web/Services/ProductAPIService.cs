using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Service.Exceptions;
using System.Net;

namespace NLayer.Web.Services
{
    public class ProductAPIService
    {
        private readonly HttpClient _httpClient;

        public ProductAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>("products/GetProductsWithCategory");
            return response.Data;
        }

        public async Task<ProductDto> SaveProductAsync(ProductDto productDto)
        {
            var response= await _httpClient.PostAsJsonAsync("products",productDto);

            if(response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();
                return responseBody.Data;
            }

            return null;

        }

        public async Task<bool> UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            var response = await _httpClient.PutAsJsonAsync("products", productUpdateDto);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return response.IsSuccessStatusCode;
        }


        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"products/{id}");

            if(response == null)
            {
                throw new ClientSideException($" There is no {id} and {typeof(Product).Name} ");
                
            }
            return response.Data;
           
        }

        public async Task<bool> RemoveProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            var errorMessage = response.StatusCode == HttpStatusCode.NotFound ?
                            "Product not found." :
                            "Error during product deletion.";
            throw new ClientSideException(errorMessage);
        }

    }
}
