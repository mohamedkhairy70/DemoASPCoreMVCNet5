using DemoASPCoreMVCNet5.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DemoASPCoreMVCNet5.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository pieRepostiory;

        public PieController(IPieRepository pieRepostiory)
        {
            this.pieRepostiory = pieRepostiory;
        }
        public IActionResult List(int catId = 0,string namePie ="")
        {
            if(catId > 0)
            {
                var result = pieRepostiory.GetAllPiesByCategory(catId);
                if(result is not null)
                    return View(result);
            }
            else if (!string.IsNullOrWhiteSpace(namePie))
            {
                var result = pieRepostiory.GetAllPiesByName(namePie);
                if (result is not null)
                    return View(result);
            }
            return View(pieRepostiory.GetAllPies());
        }

        public IActionResult Details(int id)
        {
            var result = pieRepostiory.GetPieById(id);
            if (result is null)
                return RedirectToAction("List");
            return View(result);
        }
    }
}
