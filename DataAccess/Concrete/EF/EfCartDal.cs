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
	public class EfCartDal:BaseRepository<Cart,AppDbContext>,ICartDal
	{
        public EfCartDal(AppDbContext context) : base(context)
		{
            
        }
    }
}
