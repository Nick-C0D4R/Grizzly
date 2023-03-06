using DAL.Context;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class FarmacyOfficeRepository : IContextRepository<FarmacyOffice>
    {
        private FarmacyContext _context;

        public FarmacyOfficeRepository(FarmacyContext context)
        {
            _context = context;
        }

        public IEnumerable<FarmacyOffice> GetAll() => _context.FarmacyOffices;

        public FarmacyOffice Get(int id) => _context.FarmacyOffices.Find(id);

        public void AddUpdate(FarmacyOffice farmacy)
        {
            _context.FarmacyOffices.AddOrUpdate(farmacy);
            _context.SaveChanges();
        }

        public void Remove(FarmacyOffice farmacy)
        {
            _context.FarmacyOffices.Remove(farmacy);
            _context.SaveChanges();
        }
    }
}
