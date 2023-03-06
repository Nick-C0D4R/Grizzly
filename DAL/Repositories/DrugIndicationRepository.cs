using DAL.Context;
using DAL.Interfaces;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class DrugIndicationRepository : IContextRepository<DrugIndication>
    {
        private FarmacyContext _context;
        public DrugIndicationRepository(FarmacyContext context) => _context = context;

        public IEnumerable<DrugIndication> GetAll() => _context.Indications;
        public DrugIndication Get(int id) => _context.Indications.Find(id);
        public void AddUpdate(DrugIndication drugIndication)
        {
            _context.Indications.AddOrUpdate(drugIndication);
            _context.SaveChanges();
        }
        public void Remove(DrugIndication drugIndication)
        {
            _context.Indications.Remove(drugIndication);
            _context.SaveChanges();
        }
    }
}
