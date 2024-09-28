using Core.DataAccess.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
	public class EfUserDal : BaseRepository<User, AppDbContext>, IUserDal
	{
		public EfUserDal(AppDbContext context) : base(context)
		{

		}

		public List<OperationClaim> GetClaims(User user)
		{
			var context = new AppDbContext();
			var result = from operationClaim in context.OperationClaims
						 join o in context.UserOperationClaims
						 on operationClaim.Id equals o.OperationClaimId
						 where o.UserId == user.Id
						 select
						 new OperationClaim
						 {
							 Id = operationClaim.Id,
							 Name = operationClaim.Name
						 };
			return result.ToList();
		}

        public UserGetDto GetUserById(int userId)
        {
			var context = new AppDbContext();
			var result = context.Users.Where(u => u.Id == userId).Select(u => new UserGetDto()
			{
				UserId = u.Id,
				Email = u.Email,
				FirstName = u.FirstName,
				LastName = u.LastName,
				PhoneNumber = u.PhoneNumber,	
			}).FirstOrDefault();
			return result;
        }
    }
}
