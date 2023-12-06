using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.IServices
{
    public interface ICategoryService
    {

        Task<List<CategoryModel>> GetCategories();

        Task<CategoryModel> CreateCategory(CategoryModel category);
        public Task<CategoryModel> GetCategoryById(string Id);
        Task<CategoryModel> UpdateCategory(string Id, CategoryModel category);
        Task<bool> DeleteCategory(string categoryId);
    }
}
