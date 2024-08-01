using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL
{
    public class ProductDAL
    {
        private readonly ProductManagementContext _productManagementContext;

        public ProductDAL(ProductManagementContext productManagementContext)
        {
            _productManagementContext = productManagementContext;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productManagementContext.Products.ToListAsync();
        }

        public async Task<List<Product>> GetProductsByPageAsync(int pageNum, int pageSize = 1, string by = "id", string order = "asc")
        {
            //TODO: Implement the order filter
            int offset = (pageNum - 1) * pageSize;
            if(offset > _productManagementContext.Products.Count())
                return new List<Product>();
            else
                pageSize = Math.Min(pageSize, _productManagementContext.Products.Count() - offset);
            return await _productManagementContext.Products
                                                    .Skip(offset)
                                                    .Take(pageSize).ToListAsync();
        }

        public async Task<int> GetTotalPagesAsync(int pageSize = 10)
        {
            return (int)Math.Ceiling((double)_productManagementContext.Products.Count() / pageSize);
        } 

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productManagementContext.Products.FindAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            await _productManagementContext.Products.AddAsync(product);
            await _productManagementContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _productManagementContext.Products.Update(product);
            await _productManagementContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _productManagementContext.Products.Remove(product);
            await _productManagementContext.SaveChangesAsync();
        }
    }
}
