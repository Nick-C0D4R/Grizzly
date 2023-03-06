using DAL.Context;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class UserRepository
    {
        private FarmacyContext _context;

        public UserRepository(FarmacyContext context) => _context = context;

        public IEnumerable<User> GetUsers() => _context.Users;

        public User GetUser(int id) => _context.Users.Find(id);

        public void AddUpdateUser(User user)
        {
            _context.Users.AddOrUpdate(user);
            _context.SaveChanges();
        }

        public void RemoveUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
