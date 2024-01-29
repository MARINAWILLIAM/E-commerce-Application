using DomainLayer.Entity;
using DomainLayer.Repo;
using DomainLayer.Specifications;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DataBaseHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(T entity)
        {
         await  _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            
            
           

            
            return await _dbContext.Set<T>().ToListAsync();    
            //kol product man database
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(Ispec<T> spec)
        {
           return await ApplySpec(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            //  return await _dbContext.Set<T>().Where(p=>p.Id == id).FirstOrDefaultAsync();
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpecAsync(Ispec<T> spec)
        {
            return await ApplySpec(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpecAsync(Ispec<T> spec)
        {
            return await ApplySpec(spec).CountAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        private IQueryable<T> ApplySpec(Ispec<T> spec)
        {
            return FunctionCreateQuery<T>.CreateQuery(_dbContext.Set<T>(), spec);
        }

		
	}
}
