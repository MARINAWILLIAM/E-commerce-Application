using DomainLayer;
using DomainLayer.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanal.Controllers
{
	public class BrandController : Controller
	{
		private readonly IUnitofwork _unitofwork;

		public BrandController(IUnitofwork unitofwork)
        {
			_unitofwork = unitofwork;
		}
		public async Task<IActionResult> Index()
		{
			var Brands = await _unitofwork.Repository<ProductBrandTable>().GetAllAsync();
			return View(Brands);
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProductBrandTable brand)
		{
			try
			{
				await _unitofwork.Repository<ProductBrandTable>().Add(brand);
				await _unitofwork.Complete();
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				ModelState.AddModelError("Name", "Please Enter A New Brand");
				return View("index", await _unitofwork.Repository<ProductBrandTable>().GetAllAsync());
			}
		}
		public async Task<IActionResult> Delete(int id)
		{
			var brand = await _unitofwork.Repository<ProductBrandTable>().GetByIdAsync(id);
			_unitofwork.Repository<ProductBrandTable>().Delete(brand);
			await _unitofwork.Complete();
			return RedirectToAction("index");
		}
	}
}
