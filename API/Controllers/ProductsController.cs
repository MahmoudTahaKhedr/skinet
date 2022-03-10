using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using core.Entities;
using core.Interfaces;
using core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _Productsrepo;
        private IGenericRepository<ProductBrand> _BrandRepo;
        private readonly IGenericRepository<ProductType> _TypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> Productsrepo,
        IGenericRepository<ProductBrand> BrandRepo,
        IGenericRepository<ProductType> TypeRepo,
        IMapper mapper
        )
        {
           _Productsrepo=Productsrepo;
           _BrandRepo=BrandRepo;
           _TypeRepo=TypeRepo;
           _mapper =mapper;


        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandAndSpecification();
            var products = await _Productsrepo.ListAsync(spec);
            return Ok(_mapper
                  .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
            
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandAndSpecification(id);
            var product =  await _Productsrepo.GetEntityWithSpec(spec);
            if(product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(product);
           
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _BrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _TypeRepo.ListAllAsync());
        }
    }
}