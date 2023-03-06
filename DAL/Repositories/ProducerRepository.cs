using DAL.Context;
using DAL.Interfaces;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class ProducerRepository : IContextRepository<Producer>
    {
        private FarmacyContext _context;
        public ProducerRepository(FarmacyContext context)
        {
            _context = context;
        }

        public IEnumerable<Producer> GetAll() => _context.Producers;

        public Producer Get(int id) => _context.Producers.Find(id);

        public void AddUpdate(Producer producer)
        {
            _context.Producers.AddOrUpdate(producer);
            _context.SaveChanges();
        }

        public void Remove(Producer producer)
        {
            _context.Producers.Remove(producer);
            _context.SaveChanges();
        }
    }
}
