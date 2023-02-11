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
    public class RoleRepository
    {
        private FarmacyContext _context;

        public RoleRepository(FarmacyContext context)
        {
            _context = context;
            _context.Roles.Add(new Role
            {
                Id = 1,
                RoleName = "UserNonAuth"
            });
            _context.Roles.Add(new Role
            {
                Id = 2,
                RoleName = "UserAuth"
            });
            _context.Roles.Add(new Role
            {
                Id = 3,
                RoleName = "Worker"
            });
            _context.Roles.Add(new Role
            {
                Id = 4,
                RoleName = "Admin"
            });
        }

        public IEnumerable<Role> GetRoles() => _context.Roles.ToList();

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
