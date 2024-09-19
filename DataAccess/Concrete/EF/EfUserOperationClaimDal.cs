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
	public class EfUserOperationClaimDal:BaseRepository<UserOperationClaim,AppDbContext>,IUserOperationClaimDal
	{
        public EfUserOperationClaimDal(AppDbContext context):base(context)
        {
            
        }
    }
}
