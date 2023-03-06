using DAL.Context;
using DAL.Interfaces;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class DrugTypeRepository : IContextRepository<DrugType>
    {
        private FarmacyContext _context;
        public DrugTypeRepository(FarmacyContext context) => _context = context;
        public void AddUpdate(DrugType drugType)
        {
            _context.DrugTypes.AddOrUpdate(drugType);
            _context.SaveChanges();
        }

        public DrugType Get(int id) => _context.DrugTypes.Find(id);

        public IEnumerable<DrugType> GetAll() => _context.DrugTypes;

        public void Remove(DrugType drugType)
        {
            _context.DrugTypes.Remove(drugType);
            _context.SaveChanges();
        }
    }
}
