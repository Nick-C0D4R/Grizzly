using DAL.Context;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class OrderRepository : IContextRepository<Order>
    {
        private FarmacyContext _context;

        public OrderRepository(FarmacyContext context) => _context = context;

        public IEnumerable<Order> GetAll() => _context.Orders;

        public Order Get(int id) => _context.Orders.Find(id);

        public void AddUpdate(Order order)
        {
            _context.Orders.AddOrUpdate(order);
            _context.SaveChanges();
        }

        public void Remove(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}
