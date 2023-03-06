using DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DAL.Tables
{
    public class Role : IContextTable
    {
        [Key]
        public int Id { get; set; }

        public string RoleName { get; set; }
    }
}
