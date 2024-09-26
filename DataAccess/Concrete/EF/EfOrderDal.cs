using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
    public class EfOrderDal:BaseRepository<Order,AppDbContext>,IOrderDal
    {
        public EfOrderDal(AppDbContext context):base(context)
        {
            
        }
    }
}
