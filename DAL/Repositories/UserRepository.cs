using DAL.Context;
using DAL.Interfaces;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class UserRepository : IContextRepository<User>
    {
        private FarmacyContext _context;

        public UserRepository(FarmacyContext context) => _context = context;

        public IEnumerable<User> GetAll() => _context.Users;

        public User Get(int id) => _context.Users.Find(id);

        public void AddUpdate(User user)
        {
            _context.Users.AddOrUpdate(user);
            _context.SaveChanges();
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
