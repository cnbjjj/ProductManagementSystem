using ProductManagement.DAL;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.BLL
{
    public class ProductService
    {
        private readonly ProductDAL _productDAL;

        public ProductService(ProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productDAL.GetAllProductsAsync();
        }

        public async Task<List<Product>> GetProductsByPageAsync(int pageNum, int pageSize)
        {
            return await _productDAL.GetProductsByPageAsync(pageNum, pageSize);
        }

        public async Task<int> GetTotalPagesAsync(int pageSize)
        {
            return await _productDAL.GetTotalPagesAsync(pageSize);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productDAL.GetProductByIdAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            await _productDAL.AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productDAL.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(Product product)
        {
            await _productDAL.DeleteAsync(product);
        }
    }
}
