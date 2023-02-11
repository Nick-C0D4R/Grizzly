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
    public class CartRepository
    {
        private FarmacyContext _context;

        public CartRepository(FarmacyContext context) => _context = context;

        public IEnumerable<Cart> GetCarts() => _context.Carts.ToList();

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
