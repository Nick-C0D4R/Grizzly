using DAL.Context;
using DAL.Interfaces;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class ActiveIngredientRepository : IContextRepository<ActiveIngredient>
    {
        private FarmacyContext _context;

        public ActiveIngredientRepository(FarmacyContext context) => _context = context;

        public IEnumerable<ActiveIngredient> GetAll() => _context.ActiveIngredients;

        public ActiveIngredient Get(int id) => _context.ActiveIngredients.Find(id);

        public void AddUpdate(ActiveIngredient ingredient)
        {
            _context.ActiveIngredients.AddOrUpdate(ingredient);
            _context.SaveChanges();
        }

        public void Remove(ActiveIngredient ingredient)
        {
            _context.ActiveIngredients.Remove(ingredient);
            _context.SaveChanges();
        }
    }
}
