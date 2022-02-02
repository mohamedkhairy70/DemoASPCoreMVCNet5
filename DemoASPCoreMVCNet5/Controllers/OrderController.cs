using DemoASPCoreMVCNet5.Models;
using DemoASPCoreMVCNet5.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoASPCoreMVCNet5.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly ShoppingCart shoppingCart;

        public OrderController(IOrderRepository orderRepository,ShoppingCart shoppingCart)
        {
            this.orderRepository = orderRepository;
            this.shoppingCart = shoppingCart;
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult CheckoutComplate()
        {
            ViewBag.CheckoutComplateMessage = "Complate Shopping Cart";
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order _order)
        {
            var item = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = item;

            if (shoppingCart?.ShoppingCartItems?.Count == 0)
            {
                ModelState.AddModelError("", "Can not found Item In Cart");
            }
            if (ModelState.IsValid)
            {
                orderRepository.CreateOrder(_order);
                shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplate");
            }
            return View();
        }
    }
}
