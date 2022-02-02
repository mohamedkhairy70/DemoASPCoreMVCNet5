using DemoASPCoreMVCNet5.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoASPCoreMVCNet5.Components
{
    public class CategoryList : ViewComponent
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryList(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            return View(categoryRepository.GetCategories());
        }
    }
}
