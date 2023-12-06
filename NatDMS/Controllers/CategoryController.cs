using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Naturals.Service.Service;

namespace NatDMS.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public async Task<ActionResult<CategoryModel>> DisplayCategories()
        {
            var categorieslist = await _categoryService.GetCategories();
            var mapped = _mapper.Map<List<CategoryModel>, List<CategoryViewModel>>(categorieslist);
            return View(mapped);
        }


        public ActionResult CreateCategory()
        {
            return View();

        }

        [HttpPost]
        public async Task<ActionResult<CategoryModel>>CreateCategory(CategoryViewModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<CategoryViewModel, CategoryModel>(categoryModel);

                var createcategory = await _categoryService.CreateCategory(result);

                return RedirectToAction("DisplayCategories", "Category", createcategory);

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Entered Invalid crednetials, Please Re Enter the Crendentials");
                return View(categoryModel);

            }
        }

        public async Task<ActionResult<CategoryViewModel>> EditCategory(string Id)
        {
            var category = await _categoryService.GetCategoryById(Id);
            var categoryViewModel = _mapper.Map<CategoryModel, CategoryViewModel>(category);
            return View(categoryViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditCategory(string Id, CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                //var category = await _categoryService.GetCategoryById(categoryModel.Id);

                var updatedCategory = await _categoryService.UpdateCategory(Id, categoryModel);

                return RedirectToAction("DisplayCategories", "Category", updatedCategory);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Entered Invalid credentials, Please Re Enter the Credentials");
                return View(categoryModel);
            }
        }

        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            await _categoryService.DeleteCategory(categoryId);
            return RedirectToAction("DisplayCategories", "Category");
        }
    }
}
        