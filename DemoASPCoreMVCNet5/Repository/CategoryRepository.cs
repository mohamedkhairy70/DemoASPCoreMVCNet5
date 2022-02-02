using DemoASPCoreMVCNet5.Models;
using System.Collections.Generic;

namespace DemoASPCoreMVCNet5.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDBContext context;

        public CategoryRepository(AppDBContext context)
        {
            this.context = context;
        }
        public IEnumerable<Category> GetCategories() => context.Categories;
    }
}
