using DomainLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Specifications
{
    //tgded shkal query bta3i
    public interface Ispec<T> where T : BaseEntity
        //signature comp query runa ala dbset of entity lazm tkon entity
    {
        public int Take { get; set; }
        public int Skip { get; set; }

        public bool IsPaginationEnable { get; set; }
        public Expression<Func<T,bool>> Criteria { get; set; } //where(p=>p.id==id)
        //mawgod feha value bta3at Criteria
        //ahemdd 

        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, object>> OrderBy { get; set; }
        //prop htmasl shkal elorder by eli hb3ato llorderby p=>p.name
        public Expression<Func<T, object>> OrderByDesc { get; set; }

    }
}
