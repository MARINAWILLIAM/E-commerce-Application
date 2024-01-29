using AdminPanal.Helper;
using AdminPanal.Models;
using AutoMapper;
using DomainLayer;
using DomainLayer.Entity;
using DomainLayer.Specifications;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;

namespace AdminPanal.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitofwork unitofwork;

        public ProductController(IMapper mapper,IUnitofwork unitofwork)
        {
            this.mapper = mapper;
            this.unitofwork = unitofwork;
        }
        public async Task<IActionResult> Index()
        {
			var spec = new specForincludeOnly();
			var products=await unitofwork.Repository<ProductTable>().GetAllWithSpecAsync(spec);
            var mappedproducts=mapper.Map<IReadOnlyList<ProductTable>,IReadOnlyList<ProductViewModel>>(products);
            return View(mappedproducts);
        }
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(ProductViewModel model)
		{
			var productBrand= await unitofwork.Repository<ProductBrandTable>().GetByIdAsync(model.productBrandId);
			var producttype = await unitofwork.Repository<ProductTypeTable>().GetByIdAsync(model.ProductTypeId);
			model.producttype = producttype;
			model.productBrand = productBrand;

          	if (ModelState.IsValid)
			{
				if (model.Image != null) {
					model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
				}
				else { model.PictureUrl = "images/products/hat-react2.png"; }
					

				var mappedProduct = mapper.Map<ProductViewModel, ProductTable>(model);
				await unitofwork.Repository<ProductTable>().Add(mappedProduct);
				await unitofwork.Complete();
				return RedirectToAction("Index");
			}
			return View(model);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var product = await unitofwork.Repository<ProductTable>().GetByIdAsync(id);
			var mappedProduct = mapper.Map<ProductTable, ProductViewModel>(product);
			return View(mappedProduct);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(int id, ProductViewModel model)
		{
			if (id != model.Id)
				return NotFound();
			var productBrand = await unitofwork.Repository<ProductBrandTable>().GetByIdAsync(model.productBrandId);
			var producttype = await unitofwork.Repository<ProductTypeTable>().GetByIdAsync(model.ProductTypeId);
			model.producttype = producttype;
			model.productBrand = productBrand;
			if (ModelState.IsValid)
			{
				if (model.Image != null)
				{
					if (model.PictureUrl != null)
					{
						//has picture delete and upload new 
						PictureSettings.DeleteFile(model.PictureUrl, "products");
						model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
					}
					else
					{
						//has not picture just upload new 
						model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");

					}

				}
				var mappedProduct = mapper.Map<ProductViewModel,ProductTable>(model);
				unitofwork.Repository<ProductTable>().Update(mappedProduct);
			

					var result = await unitofwork.Complete();
				if (result > 0)
					return RedirectToAction("Index");
			}
			return View(model);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var product = await unitofwork.Repository<ProductTable>().GetByIdAsync(id);
			var mappedProduct = mapper.Map<ProductTable, ProductViewModel>(product);
			return View(mappedProduct);
		}
		[HttpPost]
		public async Task<IActionResult> Delete(int id, ProductViewModel model)
		{
			if (id != model.Id)
				return NotFound();
			try
			{
				var product = await unitofwork.Repository<ProductTable>().GetByIdAsync(id);
				if (product.PictureUrl != null)
					PictureSettings.DeleteFile(product.PictureUrl, "products");

				unitofwork.Repository<ProductTable>().Delete(product);
				await unitofwork.Complete();
				return RedirectToAction("Index");

			}
			catch (System.Exception)
			{

				return View(model);
			}
		}
	}
}
