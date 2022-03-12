using core.Entities;

namespace core.Specification
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductParams productparams)
         : base(x =>
         (string.IsNullOrEmpty(productparams.Search) || x.Name.ToLower().Contains(productparams.Search)) &&
          (!productparams.BrandId.HasValue || x.ProductBrandId == productparams.BrandId) &&
         (!productparams.BrandId.HasValue || x.ProductBrandId == productparams.BrandId) &&
                    (!productparams.TypeId.HasValue || x.ProductTypeId == productparams.TypeId))
        {
        }
    }
}