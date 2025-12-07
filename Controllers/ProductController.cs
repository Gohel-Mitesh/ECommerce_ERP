using ECommerce_ERP.Data;
using ECommerce_ERP.Models.Entities;
using ECommerce_ERP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ECommerce_ERP.DTOs;

namespace ECommerce_ERP.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ProductController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            this.dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await dbContext.ProductsMaster
                .Include(p => p.Category)
            .Select(c => new ProductDto
            {
                CategoryTitle = c.Category.CategoryTitle,
                ProductId = c.ProductId,
                ProductName = c.ProductName,
                ProductSKU = c.ProductSKU,
                ProductBarcode = c.ProductBarcode,
                ProductDescription = c.ProductDescription,
                ProductPrice = c.ProductPrice,
                ProductDiscountPrice = c.ProductDiscountPrice,
                ProductChargeTax = c.ProductChargeTax,
                ProductInStock = c.ProductInStock,
                //CategoryStatus = (int)c.CategoryStatus,
                ProductStatus = c.ProductStatus.ToString(), // Convert enum to string
                ProductTags = c.ProductTags
            })
            .ToListAsync();

            return Json(new { data = products });
        }

        [HttpGet]
        public IActionResult ProductForm()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryList()
        {
            var categories = await dbContext.CategoryMaster
                .Select(c => new
                {
                    CategoryId = c.CategoryId,
                    CategoryTitle = c.CategoryTitle
                })
                .ToListAsync();

            return Json(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductForm(ProductFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Create the product
            var product = new Product
            {
                //ProductId = Guid.NewGuid(),
                ProductName = model.ProductName,
                ProductSKU = model.ProductSKU,
                ProductBarcode = model.ProductBarcode,
                ProductDescription = model.ProductDescription,
                ProductPrice = model.ProductPrice,
                ProductDiscountPrice = (decimal)model.ProductDiscountPrice,
                ProductChargeTax = model.ProductChargeTax,
                ProductInStock = model.ProductInStock,
                CategoryId = model.CategoryId,
                //ProductStatus = model.ProductStatus,
                ProductStatus = (ProductStatusEnum)model.ProductStatus,
                ProductTags = model.ProductTags,
                Variants = new List<ProductVariant>()
            };

            // Handle each variant
            if (model.Variants != null && model.Variants.Any())
            {
                foreach (var variantModel in model.Variants)
                {
                    var variant = new ProductVariant
                    {
                        //VariantId = Guid.NewGuid(),
                        ProductId = product.ProductId,
                        VariantSize = variantModel.VariantSize,
                        VariantColorHex = variantModel.VariantColorHex,
                        VariantQuantity = variantModel.VariantQuantity,
                        Photos = new List<ProductVariantPhoto>()
                    };

                    // Save each uploaded file
                    if (variantModel.Photos != null && variantModel.Photos.Any())
                    {
                        foreach (var photo in variantModel.Photos)
                        {
                            if (photo != null && photo.Length > 0)
                            {
                                // Save to wwwroot/uploads/products/
                                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "products");
                                if (!Directory.Exists(uploadsFolder))
                                    Directory.CreateDirectory(uploadsFolder);

                                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(photo.FileName)}";
                                var filePath = Path.Combine(uploadsFolder, fileName);

                                using (var fileStream = new FileStream(filePath, FileMode.Create))
                                {
                                    await photo.CopyToAsync(fileStream);
                                }

                                variant.Photos.Add(new ProductVariantPhoto
                                {
                                    //PhotoId = Guid.NewGuid(),
                                    FilePath = Path.Combine("uploads", "products", fileName)
                                });
                            }
                        }
                    }

                    product.Variants.Add(variant);
                }
            }

            // Save to database
            dbContext.ProductsMaster.Add(product);
            await dbContext.SaveChangesAsync();

            TempData["Success"] = "Product created successfully!";
            return RedirectToAction("Index");
        }


    }
}
