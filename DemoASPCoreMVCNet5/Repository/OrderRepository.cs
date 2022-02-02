using DemoASPCoreMVCNet5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoASPCoreMVCNet5.Repository
{

    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext context;
        private readonly ShoppingCart shoppingCart;

        public OrderRepository(AppDBContext context,ShoppingCart shoppingCart)
        {
            this.context = context;
            this.shoppingCart = shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPaced = System.DateTime.Now;
            var OrderDetaillist = new List<OrderDetail>();
            var shoppingCartItem = shoppingCart.ShoppingCartItems;
            foreach (var item in shoppingCartItem)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    Pie = item.Pie,
                    OrderId = order.OrderId,
                    Price = item.Pie.Price
                };
                OrderDetaillist.Add(orderDetail);
            }
            order.OrderDetails = OrderDetaillist;
            context.Orders.Add(order);

            context.SaveChanges();
        }
    }
}
