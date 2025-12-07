using ECommerce_ERP.Data;
using ECommerce_ERP.DTOs;
using ECommerce_ERP.Models;
using ECommerce_ERP.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_ERP.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await dbContext.CategoryMaster
            .Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryTitle = c.CategoryTitle,
                CategoryDescription = c.CategoryDescription,
                //CategoryStatus = (int)c.CategoryStatus,
                CategoryStatus = c.CategoryStatus.ToString(), // Convert enum to string
                CategoryImage = c.CategorImage
            })
            .ToListAsync();

            return Json(new { data = categories });
        }

        [HttpGet]
        public IActionResult CategoryForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CategoryForm(AddCategoryViewModel viewModel)
        {
            if (viewModel.CategoryId == null) // Add new category
            {
                var category = new Category // Create new entity which is model Category , for assign value from viewmodel to model
                {
                    CategoryTitle = viewModel.CategoryTitle,
                    CategorySlug = viewModel.CategorySlug,
                    CategorImage = viewModel.CategorImage,
                    CategoryParentCategory = viewModel.CategoryParentCategory,
                    CategoryStatus = (CategoryStatusEnum)viewModel.CategoryStatus,
                    CategoryDescription = viewModel.CategoryDescription
                };

                await dbContext.CategoryMaster.AddAsync(category); //pass the information to dbContext for save .

            }
            else // Update existing category
            {
                var category = await dbContext.CategoryMaster.FindAsync(viewModel.CategoryId);
                if (category == null)
                {
                    return NotFound();
                }

                category.CategoryTitle = viewModel.CategoryTitle;
                category.CategorySlug = viewModel.CategorySlug;
                category.CategorImage = viewModel.CategorImage;
                category.CategoryParentCategory = viewModel.CategoryParentCategory;
                category.CategoryStatus = (CategoryStatusEnum)viewModel.CategoryStatus;
                category.CategoryDescription = viewModel.CategoryDescription;
            }
            await dbContext.SaveChangesAsync(); //THis line is compulsory for save the changes to the database -- aa line execute thaya baad j database ma record save thay chhe.
                                                //return View();
            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        [Route("Category/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await dbContext.CategoryMaster.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var model = new AddCategoryViewModel
            {
                CategoryId = category.CategoryId,
                CategoryTitle = category.CategoryTitle,
                CategorySlug = category.CategorySlug,
                CategoryDescription = category.CategoryDescription,
                CategoryParentCategory = category.CategoryParentCategory,
                CategoryStatus = (int)category.CategoryStatus
            };

            return View("CategoryForm", model); // Assuming you use the same form for adding and editing
        }

        //[HttpPost]
        //[Route("Category/Edit")]
        //public async Task<IActionResult> Edit(AddCategoryViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View("CategoryForm", model);
        //    }

        //    var category = await dbContext.CategoryMaster.FindAsync(model.CategoryId);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    category.CategoryTitle = model.CategoryTitle;
        //    category.CategoryDescription = model.CategoryDescription;
        //    category.CategoryStatus = model.CategoryStatus;

        //    await dbContext.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await dbContext.CategoryMaster.FindAsync(id);
            if (category == null)
            {
                return Json(new { success = false, message = "Category not found" });
            }

            dbContext.CategoryMaster.Remove(category);
            await dbContext.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
