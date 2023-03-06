using DAL.Context;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class DrugRepository
    {
        private FarmacyContext _context;

        public DrugRepository(FarmacyContext context) => _context = context;

        public IEnumerable<Drug> GetDrugs() => _context.Drugs;

        public Drug GetDrug(int id) => _context.Drugs.Find(id);

        public void AddUpdateDrug(Drug drug)
        {
            _context.Drugs.AddOrUpdate(drug);
            _context.SaveChanges();
        }

        public void RemoveDrug(Drug drug)
        {
            _context.Drugs.Remove(drug);
            _context.SaveChanges();
        }
    }
}
