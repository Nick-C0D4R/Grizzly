using DAL.Context;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class DrugRepository : IContextRepository<Drug>
    {
        private FarmacyContext _context;

        public DrugRepository(FarmacyContext context) => _context = context;

        public IEnumerable<Drug> GetAll() => _context.Drugs;

        public Drug Get(int id) => _context.Drugs.Find(id);

        public void AddUpdate(Drug drug)
        {
            _context.Drugs.AddOrUpdate(drug);
            _context.SaveChanges();
        }

        public void Remove(Drug drug)
        {
            _context.Drugs.Remove(drug);
            _context.SaveChanges();
        }
    }
}
