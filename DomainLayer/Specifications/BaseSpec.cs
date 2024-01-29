using DomainLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Specifications
{
    public class BaseSpec<T> : Ispec<T> where T : BaseEntity
    {
        //hy3mal obj alshan yhot value ll dol
        //Create obj gwah hagtan dol 
        public Expression<Func<T, bool>> Criteria { get ; set; }
        public List<Expression<Func<T, object>>> Includes { get; set ; }  = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Take { get ; set  ; }
        public int Skip { get  ; set; }
        public bool IsPaginationEnable { get ; set ; }
        public void ApplyPagnation(int skip, int take)
        {
            IsPaginationEnable = true;
        Skip = skip;
        Take = take;
        }

        public BaseSpec()
        {
            //Includes.Add();
            //Includes.Add();

             
        }
        public BaseSpec(Expression<Func<T, bool>> criteria)//AYHAD HY3MAL OBJ USE THIS CTOR HYB3AT CRITERIA 
        {

            Criteria = criteria;
        }
        public void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {

            OrderBy = OrderByExpression;//p=>p.priceasc



        }
        public void AddOrderByDESC(Expression<Func<T, object>> OrderByDescExpression)
        {

            OrderByDesc = OrderByDescExpression;

            //just set ll orderby two

        }
    }
}


