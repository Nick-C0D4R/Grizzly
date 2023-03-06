using DAL.Context;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class CartRepository : IContextRepository<Cart>
    {
        private FarmacyContext _context;

        public CartRepository(FarmacyContext context) => _context = context;

        public IEnumerable<Cart> GetAll() => _context.Carts;

        public Cart Get(int id) => _context.Carts.Find(id);

        public void AddUpdate(Cart cart)
        {
            _context.Carts.AddOrUpdate(cart);
            _context.SaveChanges();
        }

        public void Remove(Cart cart)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }
    }
}
