using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ProductManagement.BLL;
using ProductManagement.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProductManagement.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Page", new { PageNum = 1 });
        }

        public async Task<IActionResult> All()
        {
            List<Product> products = await _productService.GetAllProductsAsync();
            return View("Index", products);
        }

        [HttpGet("{controller}/Page/{PageNum}/{PageSize?}")]
        public async Task<IActionResult> Page(int PageNum, int PageSize = 5)
        {
            List<Product> products = await _productService.GetProductsByPageAsync(PageNum, PageSize);
            ViewBag.PageNum = PageNum;
            ViewBag.PageSize = PageSize;
            ViewBag.TotalPages = await _productService.GetTotalPagesAsync(PageSize);
            return View("Index", products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(product);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "An error occured while creating the product");
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                ModelState.AddModelError("", "Product not exsits");
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(product);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                ModelState.AddModelError("", "Product not exsits");
            await _productService.DeleteProductAsync(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ImportProducts()
        {
            try
            {
                List<Product> products = await GetProducts();
                if (products != null)
                {
                    foreach (Product p in products)
                    {
                        p.id = 0;
                        await _productService.AddProductAsync(p);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToAction("Index");
        }

        private async Task<List<Product>?> GetProducts()
        {
            HttpClient client = new HttpClient();
            var res = await client.GetStringAsync("https://fakestoreapi.com/products");
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(res);
            return products;
        }

        [HttpPost("{controller}/ChangeQuantity/{id}")]
        public async Task<IActionResult> ChangeQuantity(int id, [FromBody] int quantity)
        {
            Product product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound(new { id = id, quantity = quantity });
            product.quantity = quantity;
            await _productService.UpdateProductAsync(product);
            return Ok(new { id = product.id, quantity = product.quantity });
        }
    }
}
