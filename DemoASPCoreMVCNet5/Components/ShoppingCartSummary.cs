using DemoASPCoreMVCNet5.Repository;
using DemoASPCoreMVCNet5.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DemoASPCoreMVCNet5.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
        }
        public IViewComponentResult Invoke()
        {
            var item = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = item;

            ShoppingCartViewModel shoppingCartView = new ShoppingCartViewModel
            {
                ShoppingCarts = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartView);
        }
    }
}
