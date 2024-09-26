using Business.Abstract;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager(IOrderDal orderDal,IProductDal productDal) : IOrderService
    {
        private readonly IOrderDal _orderDal = orderDal;
        readonly IProductDal _productDal = productDal;
        public IDataResult<List<OrderDto>> GetOrdersByUserId(int userId)
        {
            // Müvafiq istifadəçi üçün sifarişləri əldə et
            var orders = _orderDal.GetAll(o => o.UserId == userId);

            if (orders == null || !orders.Any())
            {
                return new ErrorDataResult<List<OrderDto>>("Sifarişlər tapılmadı");
            }

            // Sifarişləri DTO formatına çevir
            var orderDtos = orders.Select(o => new OrderDto
            {
                OrderId = o.Id,
                ProductId = o.ProductId,
                ProductName = _productDal.Get(p => p.Id == o.ProductId)?.Name ?? "Məhsul tapılmadı",
                Price = o.Price,
                Quantity = o.Quantity,
                OrderDate = o.OrderDate,
            }).ToList();

            return new SuccessDataResult<List<OrderDto>>(orderDtos);
        }
    }
}
