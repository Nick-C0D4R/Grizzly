using DAL.Context;
using DAL.Interfaces;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class RoleRepository : IContextRepository<Role>
    {
        private FarmacyContext _context;

        public RoleRepository(FarmacyContext context) => _context = context;

        public IEnumerable<Role> GetAll() => _context.Roles;

        public Role Get(int id) => _context.Roles.Find(id);

        public void AddUpdate(Role role)
        {
            _context.Roles.AddOrUpdate(role);
            _context.SaveChanges();
        }

        public void Remove(Role role)
        {
            _context.Roles.Remove(role);
            _context.SaveChanges();
        }
    }
}
