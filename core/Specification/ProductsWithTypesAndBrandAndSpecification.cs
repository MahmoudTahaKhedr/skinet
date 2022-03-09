using System;
using System.Linq.Expressions;
using core.Entities;

namespace core.Specification
{
    public class ProductsWithTypesAndBrandAndSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandAndSpecification()
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
        }

        public ProductsWithTypesAndBrandAndSpecification(int id) : base(x=>x.Id==id)
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);  
        }
    }
}