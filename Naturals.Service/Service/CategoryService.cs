using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public CategoryService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<CategoryModel> CreateCategory(CategoryModel category)
        {
            var result = await _httpClientWrapper.PostAsync<CategoryModel>("/Category",category);

            return result;
        }

        public async Task<List<CategoryModel>> GetCategories()
        {
            var result = await _httpClientWrapper.GetAsync<List<CategoryModel>>("/Category/");
            return result;
        }
        public async Task<CategoryModel> GetCategoryById(string Id)
        {
            var result = await _httpClientWrapper.GetByIdAsync<CategoryModel>("/Category", Id);
            return result;
        }
        public async Task<CategoryModel> UpdateCategory(string Id, CategoryModel category)
        {
            var result = await _httpClientWrapper.PutAsync<CategoryModel>("/Category", Id, category);
            return result;
        }

        public async Task<bool> DeleteCategory(string categoryId)
        {
            try
            {
                var isDeleted = await _httpClientWrapper.DeleteAsync("/Category", categoryId);

                if (isDeleted)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while deleting the retailor.");
            }
        }
    }
}
