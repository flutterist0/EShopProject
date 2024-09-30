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
    public class OrderManager(IOrderDal orderDal,IProductDal productDal,ICartDal cartDal,ICartItemDal cartItemDal,IShippingAddressDal shippingAddressDal) : IOrderService
    {
        private readonly IOrderDal _orderDal = orderDal;
        private readonly IProductDal _productDal = productDal;
        private readonly ICartDal _cartDal = cartDal;
        private readonly ICartItemDal _cartItemDal = cartItemDal;
        private readonly IShippingAddressDal _shippingAddressDal = shippingAddressDal;

        public IResult AddOrder(int userId, int shippingAddressId)
        {
            var cart = _cartDal.GetCartByUserId(userId);
            if (cart == null)
            {
                return new ErrorResult("Səbət tapılmadı");
            }

            var items = _cartItemDal.GetAll(ci => ci.CartId == cart.Id);

            var shippingAddress = _shippingAddressDal.Get(s => s.Id == shippingAddressId && s.UserId == userId);
            if (shippingAddress == null)
            {
                return new ErrorResult("Shipping ünvanı tapılmadı və ya istifadəçiyə aid deyil");
            }

            foreach (var item in items)
            {
                var order = new Order
                {
                    UserId = userId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    OrderDate = DateTime.Now,
                    ShippingAddressId = shippingAddressId
                };

                _orderDal.Add(order);

                var product = _productDal.Get(p => p.Id == item.ProductId);
                if (product != null)
                {
                    product.Stock -= item.Quantity;
                    if (product.Stock <= 0)
                    {
                        product.IsDelete = true;
                        _productDal.Delete(product);
                    }
                    else
                    {
                        _productDal.Update(product);
                    }
                }
            }
            _cartItemDal.DeleteRange(items);
            return new SuccessResult("Sifariş uğurla əlavə edildi");
        }

        public IDataResult<List<OrderDto>> GetOrdersByUserId(int userId)
        {
            var orders = _orderDal.GetAll(o => o.UserId == userId);

            if (orders == null || !orders.Any())
            {
                return new ErrorDataResult<List<OrderDto>>("Sifarişlər tapılmadı");
            }

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
