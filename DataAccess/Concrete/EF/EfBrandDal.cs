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
    public class EfBrandDal:BaseRepository<Brand,AppDbContext>,IBrandDal
    {
        public EfBrandDal(AppDbContext context) : base(context)
		{
            
        }
    }
}
