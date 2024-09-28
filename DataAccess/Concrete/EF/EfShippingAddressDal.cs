using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
	public class EfShippingAddressDal:BaseRepository<ShippingAddress,AppDbContext>,IShippingAddressDal
	{
        public EfShippingAddressDal(AppDbContext context) : base(context)
		{
            
        }

        public ShippingAddressGetAllDto GetAllByUserIdShipingAdresses(int userId)
        {
            AppDbContext context = new();
            var shippingAddress = context.ShippingAddresses.Where(s => s.UserId == userId && s.IsDelete == false).Select(s=> 
            new ShippingAddressGetAllDto
            {
                ShippingAddressId = s.Id,
                UserId = s.UserId,
                City = s.City,
                CountryId = s.CountryId,
                State = s.State,
                Street = s.Street,
                PhoneNumber = s.PhoneNumber,
            }).FirstOrDefault();
           return shippingAddress;
        }
    }
}
