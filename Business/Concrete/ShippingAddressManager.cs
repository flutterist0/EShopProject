using Business.Abstract;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class ShippingAddressManager(IShippingAddressDal shippingAddressDal) : IShippingAddressService
	{
		private readonly IShippingAddressDal _shippingAddressDal = shippingAddressDal;
		public IResult Add(ShippingAddressAddDto shippingAddressAddDto)
		{
			var shippingAddress = new ShippingAddress
			{
				FirstName = shippingAddressAddDto.FirstName,
				LastName = shippingAddressAddDto.LastName,
				Email = shippingAddressAddDto.Email,
				PhoneNumber = shippingAddressAddDto.PhoneNumber,
				City = shippingAddressAddDto.City,
				State = shippingAddressAddDto.State,
				ZipCode = shippingAddressAddDto.ZipCode,
				UserId = shippingAddressAddDto.UserId,
				CountryId = shippingAddressAddDto.CountryId,
				Street = shippingAddressAddDto.Street,
			};
			_shippingAddressDal.Add(shippingAddress);
			return new SuccessResult("Shipping address has been successfully added.");
		}

        public IResult Delete(int userId)
        {
            ShippingAddress deleteShippingAddress = null;
            ShippingAddress result = _shippingAddressDal.Get(a =>  a.IsDelete == false&& a.UserId==userId);
            if (result != null)
            {
                deleteShippingAddress = result;
				deleteShippingAddress.IsDelete = true;
                _shippingAddressDal.Delete(deleteShippingAddress);
                return new SuccessResult("deleted");
            }
            else

                return new ErrorResult("silinmedi");
        }

        public IDataResult<ShippingAddressGetAllDto> GetByUserIdShippingAddresses(int userId)
        {
            var result = _shippingAddressDal.GetAllByUserIdShipingAdresses(userId);
			if (result != null) 
			{ 
				return new SuccessDataResult<ShippingAddressGetAllDto>(result);

			}else
				return new ErrorDataResult<ShippingAddressGetAllDto>(result,"Xeta");
        }
    }
}
