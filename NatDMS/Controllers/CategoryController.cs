using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NatDMS.Models;
using Natural.Core.IServices;
using Natural.Core.Models;
using Naturals.Service.Service;

namespace NatDMS.Controllers
{

    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CategoryController(ICategoryService categoryService, IMapper mapper, IConfiguration configuration)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<ActionResult<CategoryModel>> DisplayCategories(int page = 1)
        {
            var categoryresult = await _categoryService.GetCategories();
            var categoryPgn = new PageNation<CategoryModel>(categoryresult, _configuration, page);
            var paginatedData = categoryPgn.GetPaginatedData(categoryresult);
            ViewBag.Pages = categoryPgn;
            var mapped = _mapper.Map<List<CategoryModel>, List<CategoryViewModel>>(paginatedData);
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
        