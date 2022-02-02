using DemoASPCoreMVCNet5.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DemoASPCoreMVCNet5.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            this.pieRepository = pieRepository;
        }
        public IActionResult Index()
        {
            return View(pieRepository.GetAllPies());
        }
    }
}
