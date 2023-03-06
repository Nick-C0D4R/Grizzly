using DAL.Context;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class RoleRepository
    {
        private FarmacyContext _context;

        public RoleRepository(FarmacyContext context) => _context = context;

        public IEnumerable<Role> GetRoles() => _context.Roles;

        public Role GetRole(int id) => _context.Roles.Find(id);

        public void AddUpdateRole(Role role)
        {
            _context.Roles.AddOrUpdate(role);
            _context.SaveChanges();
        }

        public void RemoveRole(Role role)
        {
            _context.Roles.Remove(role);
            _context.SaveChanges();
        }
    }
}
