using DomainLayer.Entity;
using DomainLayer.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repo
{
    public interface IGenericRepository<T> where T : BaseEntity
        //Msh htkon ay class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
		
		Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(Ispec<T> spec);
        Task<T> GetEntityWithSpecAsync(Ispec<T> spec);
        Task<int> GetCountWithSpecAsync(Ispec<T> spec);
        Task Add(T entity);
        void Update(T entity);//t record
        void Delete(T entity);
    }
}
