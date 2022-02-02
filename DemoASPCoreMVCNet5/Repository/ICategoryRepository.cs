using DemoASPCoreMVCNet5.Models;
using System.Collections.Generic;

namespace DemoASPCoreMVCNet5.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
    }
}
