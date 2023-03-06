using DAL.Context;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class OrderRepository
    {
        private FarmacyContext _context;

        public OrderRepository(FarmacyContext context) => _context = context;

        public IEnumerable<Order> GetOrders() => _context.Orders;

        public Order GetOrder(int id) => _context.Orders.Find(id);

        public void AddUpdateOrder(Order order)
        {
            _context.Orders.AddOrUpdate(order);
            _context.SaveChanges();
        }

        public void RemoveOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}
