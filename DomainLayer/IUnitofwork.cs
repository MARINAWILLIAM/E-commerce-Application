using DomainLayer.Entity;
using DomainLayer.Entity.Order;
using DomainLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public interface IUnitofwork:IAsyncDisposable 
    {
        //public IGenericRepository<ProductTable> ProductRepo { get; set; }
        //public IGenericRepository<ProductBrandTable> ProductBrandRepo { get; set; }
        //public IGenericRepository<ProductTypeTable> ProductTypeRepo { get; set; }
        //public IGenericRepository<DeliveryMethod> DeliveryMethodRepo { get; set; }
        //public IGenericRepository<OrderItem> OrderItemRepo { get; set; }
        //public IGenericRepository<Order> OrderRepo { get; set; }
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
