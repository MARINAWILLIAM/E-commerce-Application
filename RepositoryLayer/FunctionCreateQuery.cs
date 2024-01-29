using DomainLayer.Entity;
using DomainLayer.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public static class FunctionCreateQuery<T> where T : BaseEntity
    {
        public static IQueryable<T> CreateQuery(IQueryable<T> EntryPoint,Ispec<T> spec)
        {
            //dbset no3ha             spec
            var query = EntryPoint;//_dbcontext.set<Product>
            if (spec.Criteria != null)//p=>p.id==1
            {
                query=query.Where(spec.Criteria); 

            }//_dbcontext.set<Product>.Where(p=>p.id==1) awl spec
           if(spec.OrderBy != null)
            {
                query=query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }
            if (spec.IsPaginationEnable)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }




            query = spec.Includes.Aggregate(query, (CurentQuery, QuerywithIncludes) => CurentQuery.Include(QuerywithIncludes));
            return query;


























        }
    }
}
