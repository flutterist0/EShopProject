using Core.DataAccess.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
	public class EfOperationClaimDal:BaseRepository<OperationClaim,AppDbContext>,IOperationClaimDal
	{
        public EfOperationClaimDal(AppDbContext context):base(context) 
        {
            
        }
    }
}
