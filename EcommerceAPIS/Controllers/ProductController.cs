using AutoMapper;
using DomainLayer;
using DomainLayer.Entity;
using DomainLayer.Repo;
using DomainLayer.Specifications;
using EcommerceAPIS.Dtos;
using EcommerceAPIS.Errors;
using EcommerceAPIS.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EcommerceAPIS.Controllers
{
   
    public class ProductController : BaseController
    {
        //private readonly IGenericRepository<ProductTable> _productrepo;
        //private readonly IGenericRepository<ProductBrandTable> _productBrandRepo;
        //private readonly IGenericRepository<ProductTypeTable> _productTypeRepo;
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;

        //shkal repo ay had ayz repo lazm ynfaz interface mash hy3raf yb3at elrepo
        public ProductController(
            //IGenericRepository<ProductTable> productrepo,
            //IGenericRepository<ProductBrandTable> productBrandRepo,
            //IGenericRepository<ProductTypeTable> productTypeRepo,
            IUnitofwork unitofwork,
            IMapper mapper)
        {
            //_productrepo = productrepo;
            //_productBrandRepo = productBrandRepo;
            //_productTypeRepo = productTypeRepo;
           _unitofwork = unitofwork;
            _mapper = mapper;
            //obj eli hyt3malo create 
        }
        // [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [CachedAttribute(600)]//not exist i will make it
        [HttpGet]
        //to tell endpoint that excute or not you must use 
        //action fliter

        public async Task<ActionResult<Pagination<ProductDto>>> GetProduct([FromQuery]productwithspecparams specparams)
        {
            var spec=new ProductSpecIncludes(specparams);
            var specforcount = new productspecspecialforcount(specparams);
            var products= await _unitofwork.Repository<ProductTable>().GetAllWithSpecAsync(spec);
           
            //hyrga3 kol products
            //ma3ya kol products
            //ayzen nrga3 response
            var data = _mapper.Map<IReadOnlyList<ProductTable>, IReadOnlyList<ProductDto>>(products);
            //shyla eldata eli rag3a b3d skip weltake
           
            var count = await _unitofwork.Repository<ProductTable>().GetCountWithSpecAsync(specforcount);
            return Ok(new Pagination<ProductDto>(specparams.pageIndex,specparams.PageSize, count, data));
        }
        [CachedAttribute(600)]//not exist i will make it
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrandTable>>> GetBrands()
        {

            var brands = await _unitofwork.Repository<ProductBrandTable>().GetAllAsync();
            
            return Ok(brands);
        }
        [CachedAttribute(600)]//not exist i will make it
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductTypeTable>>> GetTypes()
            {

                var types = await _unitofwork.Repository<ProductTypeTable>().GetAllAsync();
               
                return Ok(types);
            }
        [CachedAttribute(600)]//not exist i will make it
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
         [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {

            var spec = new ProductSpecIncludes(id);
            var product= await _unitofwork.Repository<ProductTable>().GetEntityWithSpecAsync(spec);
            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            //hyrga3 kol products
            //ma3ya kol products
            //ayzen nrga3 response
            return Ok(_mapper.Map<ProductTable,ProductDto>(product));
        }
    }
}
