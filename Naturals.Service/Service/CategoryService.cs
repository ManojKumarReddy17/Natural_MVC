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
    }
}
