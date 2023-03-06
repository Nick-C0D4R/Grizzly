using DAL.Context;
using DAL.Interfaces;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class ApplicationTypeRepository : IContextRepository<DrugApplicationType>
    {
        private FarmacyContext _context;

        public ApplicationTypeRepository(FarmacyContext context) => _context = context;

        public IEnumerable<DrugApplicationType> GetAll() => _context.ApplicationTypes;
        public DrugApplicationType Get(int id) => _context.ApplicationTypes.Find(id);
        public void AddUpdate(DrugApplicationType drugApplicationType)
        {
            _context.ApplicationTypes.AddOrUpdate(drugApplicationType);
            _context.SaveChanges();
        }
        public void Remove(DrugApplicationType drugApplicationType)
        {
            _context.ApplicationTypes.Remove(drugApplicationType);
            _context.SaveChanges();
        }
    }
}
