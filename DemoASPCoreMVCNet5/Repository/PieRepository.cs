using DemoASPCoreMVCNet5.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DemoASPCoreMVCNet5.Repository
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDBContext context;

        public PieRepository(AppDBContext context)
        {
            this.context = context;
        }
        public IEnumerable<Pie> GetAllPies() => context.Pies.Include(p => p.Category);

        public IEnumerable<Pie> GetAllPiesByCategory(int IdCat)
            => context.Pies.Include(p => p.Category).Where(p => p.CategoryId == IdCat);

        public IEnumerable<Pie> GetAllPiesByName(string namePie)
            => context.Pies.Include(p => p.Category).Where(p => p.Name.Contains(namePie));

        public Pie GetPieById(int id) => context.Pies
            .Include(p => p.Category)
            .FirstOrDefault(p => p.PieId == id);
    }
}
