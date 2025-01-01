using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; set; }
        public Address Address { get; set; }
        public string BuyerId { get; set; }
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order(Address address, string buyerId)
        {
            CreatedDate = DateTime.Now;
            Address = address;
            BuyerId = buyerId;
            _orderItems = new();
        }

        public void AddOrderItem(string productId, string productName, decimal price, string pictuteUrl)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictuteUrl, price);

            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);

    }
}
