using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class User : IContextTable
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public string Login { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime JoinDate { get; set; }

        public virtual Role Role { get; set; }
    }
}
