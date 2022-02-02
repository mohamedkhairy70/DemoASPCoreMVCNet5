using DemoASPCoreMVCNet5.Models;
using System.Collections.Generic;

namespace DemoASPCoreMVCNet5.Repository
{
    public interface IPieRepository
    {
        IEnumerable<Pie> GetAllPies();
        IEnumerable<Pie> GetAllPiesByName(string namePie);
        IEnumerable<Pie> GetAllPiesByCategory(int IdCat);
        Pie GetPieById(int id);

    }
}
