using DemoASPCoreMVCNet5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoASPCoreMVCNet5.Repository
{
    public class ShoppingCart
    {
        private readonly AppDBContext context;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(AppDBContext context)
        {
            this.context = context;
        }
        public static ShoppingCart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var serviceContext = service.GetService<AppDBContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(serviceContext) { ShoppingCartId = cartId };
        }
        public void AddToCart(Pie pie,int Amount = 1)
        {
            var oldShop = context.ShoppingCartItems
                .FirstOrDefault(p => p.Pie.PieId == pie.PieId && p.ShoppingCartId == ShoppingCartId);

            if(oldShop is null)
            {
                oldShop = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = Amount
                };
                context.ShoppingCartItems.Add(oldShop);

            }
            else
            {
                oldShop.Amount += Amount;
            }
            context.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    context.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            context.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            if (ShoppingCartItems is null || ShoppingCartItems?.Count == 0)
                ShoppingCartItems =
                       context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Pie)
                           .ToList();
            return ShoppingCartItems;
        }

        public void ClearCart()
        {
            var cartItems = context
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            context.ShoppingCartItems.RemoveRange(cartItems);

            context.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }

    }
}
