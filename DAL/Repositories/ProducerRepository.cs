using DAL.Context;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProducerRepository
    {
        private FarmacyContext _context;
        public ProducerRepository(FarmacyContext context)
        {
            _context = context;
        }

        public IEnumerable<Producer> GetProducers() => _context.Producers.ToList();

        public Producer GetProducer(int id) => _context.Producers.Find(id);

        public void AddUpdateProducer(Producer producer)
        {
            _context.Producers.AddOrUpdate(producer);
            _context.SaveChanges();
        }

        public void RemoveProducer(Producer producer)
        {
            _context.Producers.Remove(producer);
            _context.SaveChanges();
        }
    }
}
