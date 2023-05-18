using DAL.Tables;
using System;
using System.Data.Entity;
using System.Linq;

namespace DAL.Context
{
    public class FarmacyContext : DbContext
    {
        
        public FarmacyContext()
            : base("name=FarmacyContext")
        {
        }

        public DbSet<Producer> Producers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PharmacyOffice> FarmacyOffices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<DrugType> DrugTypes { get; set; }
        public DbSet<ActiveIngredient> ActiveIngredients { get; set; }
        public DbSet<DrugApplicationType> ApplicationTypes { get; set; }
        public DbSet<DrugIndication> Indications { get; set; }
        public DbSet<DrugContraIndication> ContraIndications { get; set; }
    }

}