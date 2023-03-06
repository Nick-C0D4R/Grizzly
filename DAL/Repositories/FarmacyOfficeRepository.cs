using DAL.Context;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class FarmacyOfficeRepository
    {
        private FarmacyContext _context;

        public FarmacyOfficeRepository(FarmacyContext context)
        {
            _context = context;
        }

        public IEnumerable<FarmacyOffice> GetOffices() => _context.FarmacyOffices;

        public FarmacyOffice GetOffice(int id) => _context.FarmacyOffices.Find(id);

        public void AddUpdateFarmacy(FarmacyOffice farmacy)
        {
            _context.FarmacyOffices.AddOrUpdate(farmacy);
            _context.SaveChanges();
        }

        public void RemoveFarmacy(FarmacyOffice farmacy)
        {
            _context.FarmacyOffices.Remove(farmacy);
            _context.SaveChanges();
        }
    }
}
