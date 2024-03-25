using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Service.Exceptions;
using System.Net;

namespace NLayer.Web.Services
{
    public class CategoryAPIService
    {
        private readonly HttpClient _httpClient;

        public CategoryAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDto>> GetAllAsyncCategories()
        {
            var response= await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryDto>>>("categories");
            if(response == null)
            {
                throw new ClientSideException($" There is no data ");
            }
            return response.Data;
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<CategoryDto>>($"categories/{id}");
            if (response == null)
            {
                throw new ClientSideException($" There is no {id} and {typeof(Category).Name} ");

            }
            return response.Data;
        }

        public async Task<CategoryDto> SaveCategoriesAsync(CategoryDto categoryDto)
        {
            var response=await _httpClient.PostAsJsonAsync("categories", categoryDto);
            if(response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<CategoryDto>>();
                return responseBody.Data;
            }
            throw new ClientSideException("Category could not Saved");

        }

        public async Task<CategoryDto>UpdateCategoriesAsync(CategoryDto categoryDto)
        {
            var response = await _httpClient.PutAsJsonAsync("categories",categoryDto);
            if(response.IsSuccessStatusCode)
            {
                return categoryDto;
            }
            throw new ClientSideException("Category could not be updated");
        }

        public async Task<bool>RemoveCategoriesAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}");
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorMessage = response.StatusCode == HttpStatusCode.NotFound ?
                            "Category not found." :
                            "Error during category deletion.";
                throw new ClientSideException(errorMessage);
            }
            
        }


    }
}
