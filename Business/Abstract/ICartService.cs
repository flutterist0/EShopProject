using Core.Helpers.Results.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ICartService
	{
		IResult AddCart(int productId, int userId, int quantity);
		IResult DeleteCart(int userId, int productId);
		IDataResult<List<CartItemDto>> GetAllCarts(int userId);
		IDataResult<CheckoutDto> GetCheckoutDetails(int userId);

    }
}
