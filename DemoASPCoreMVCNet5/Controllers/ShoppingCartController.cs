using DemoASPCoreMVCNet5.Repository;
using DemoASPCoreMVCNet5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoASPCoreMVCNet5.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart shoppingCart;
        private readonly IPieRepository pieRepository;

        public ShoppingCartController(ShoppingCart shoppingCart,IPieRepository pieRepository)
        {
            this.shoppingCart = shoppingCart;
            this.pieRepository = pieRepository;
        }
        public IActionResult Index()
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
        public IActionResult AddToShoppingCart(int pieId)
        {
            var pie = pieRepository.GetPieById(pieId);

            if(pie is not null)
                shoppingCart.AddToCart(pie);
            else
                return RedirectToAction("Index", "Home");

            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromShoppingCart(int pieId)
        {
            var pie = pieRepository.GetPieById(pieId);

            if(pie is not null)
                shoppingCart.RemoveFromCart(pie);
            else
                return RedirectToAction("Index", "Home");
            
            return RedirectToAction("Index");
        }
    }
}
