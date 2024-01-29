using DomainLayer;
using DomainLayer.Entity.Order;
using DomainLayer.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanal.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitofwork unitOfWork;

        public OrderController(IUnitofwork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecifications();
            var orders = await unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return View(orders);
        }
        public async Task<IActionResult> GetUserOrders(string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecifications(buyerEmail);
            var orders = await unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            return View("Index", orders);
        }
    }
}
