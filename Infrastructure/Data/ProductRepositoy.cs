using System.Collections.Generic;
using System.Threading.Tasks;
using core.Entities;
using core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepositoy : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepositoy(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
           return await _context.Products
           .Include(x=>x.ProductBrand)
           .Include(x=>x.ProductType)
           .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
           return await _context.Products
           .Include(x=>x.ProductBrand)
           .Include(x=>x.ProductType)
           .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}