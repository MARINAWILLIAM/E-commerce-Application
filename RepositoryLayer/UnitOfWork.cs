using DomainLayer;
using DomainLayer.Entity;
using DomainLayer.Entity.Order;
using DomainLayer.Repo;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DataBaseHandler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class UnitOfWork : IUnitofwork
    {
        private readonly StoreContext _context;
        //private Dictionary<string,GenericRepository<BaseEntity>> _repository;
      private Hashtable _repository;
        //hashtable non generic object object
        //repo table    
        //kol repo eli hytlobi fe runtime

        //lma had ytlob obj man unitof work hy3mal eh hy3adi ala prop h3mlha intialzation

        //public IGenericRepository<ProductTable> ProductRepo { get ; set; }
        //public IGenericRepository<ProductBrandTable> ProductBrandRepo { get; set; }
        //public IGenericRepository<ProductTypeTable> ProductTypeRepo { get; set; }
        //public IGenericRepository<DeliveryMethod> DeliveryMethodRepo { get ; set; }
        //public IGenericRepository<OrderItem> OrderItemRepo { get; set ; }
        //public IGenericRepository<Order> OrderRepo { get; set; }
        public UnitOfWork(StoreContext context)
        {
            _context = context;
            _repository=new Hashtable();
            //intialzation



        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
           //hgeb tentity no3ha eh
           var type=typeof(TEntity).Name; //product
            if (!_repository.ContainsKey(type))
            {
              var repo=new GenericRepository<TEntity>(_context); //b3tha bedi
                _repository.Add(type, repo);
            }
            return _repository[type]as IGenericRepository<TEntity>;

        }
        public async Task<int> Complete()
        {
          
        return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
             await _context.DisposeAsync();
        }

       
    }
}
