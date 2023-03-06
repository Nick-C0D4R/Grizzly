using DAL.Context;
using DAL.Interfaces;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class ContraIndicationRepository : IContextRepository<DrugContraIndication>
    {
        private FarmacyContext _context;

        public ContraIndicationRepository( FarmacyContext context ) => _context = context;
        public IEnumerable<DrugContraIndication> GetAll() => _context.ContraIndications;
        public DrugContraIndication Get(int id) => _context.ContraIndications.Find(id);
        public void AddUpdate(DrugContraIndication contraIndication)
        {
            _context.ContraIndications.AddOrUpdate(contraIndication);
            _context.SaveChanges();
        }
        public void Remove(DrugContraIndication contraIndication)
        {
            _context.ContraIndications.Remove(contraIndication);
            _context.SaveChanges();
        }
    }
}
