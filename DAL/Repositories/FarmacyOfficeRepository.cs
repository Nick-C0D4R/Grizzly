using DAL.Context;
using DAL.Interfaces;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class FarmacyOfficeRepository : IContextRepository<PharmacyOffice>
    {
        private FarmacyContext _context;

        public FarmacyOfficeRepository(FarmacyContext context)
        {
            _context = context;
        }

        public IEnumerable<PharmacyOffice> GetAll() => _context.FarmacyOffices;

        public PharmacyOffice Get(int id) => _context.FarmacyOffices.Find(id);

        public void AddUpdate(PharmacyOffice farmacy)
        {
            _context.FarmacyOffices.AddOrUpdate(farmacy);
            _context.SaveChanges();
        }

        public void Remove(PharmacyOffice farmacy)
        {
            _context.FarmacyOffices.Remove(farmacy);
            _context.SaveChanges();
        }
    }
}
