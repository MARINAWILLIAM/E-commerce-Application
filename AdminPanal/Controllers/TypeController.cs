using DomainLayer.Entity;
using DomainLayer;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanal.Controllers
{
	public class TypeController : Controller
	{
        private readonly IUnitofwork _unitofwork;

        public TypeController(IUnitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task<IActionResult> Index()
        {
            var types = await _unitofwork.Repository<ProductTypeTable>().GetAllAsync();
            return View(types);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductTypeTable types)
        {
            try
            {
                await _unitofwork.Repository<ProductTypeTable>().Add(types);
                await _unitofwork.Complete();
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("Name", "Please Enter A New Type");
                return View("index", await _unitofwork.Repository<ProductTypeTable>().GetAllAsync());
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _unitofwork.Repository<ProductTypeTable>().GetByIdAsync(id);
            _unitofwork.Repository<ProductTypeTable>().Delete(type);
            await _unitofwork.Complete();
            return RedirectToAction("index");
        }
    }
}
