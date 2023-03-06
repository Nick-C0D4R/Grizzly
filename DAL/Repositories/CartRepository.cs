using DAL.Context;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class CartRepository
    {
        private FarmacyContext _context;

        public CartRepository(FarmacyContext context) => _context = context;

        public IEnumerable<Cart> GetCarts() => _context.Carts;

        public Cart GetCart(int id) => _context.Carts.Find(id);

        public void AddUpdateCart(Cart cart)
        {
            _context.Carts.AddOrUpdate(cart);
            _context.SaveChanges();
        }

        public void RemoveUpdateCart(Cart cart)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }
    }
}
