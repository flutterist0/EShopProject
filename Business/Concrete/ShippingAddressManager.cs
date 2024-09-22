using Business.Abstract;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
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
			};
			_shippingAddressDal.Add(shippingAddress);
			return new SuccessResult("Shipping address has been successfully added.");
		}
	}
}
