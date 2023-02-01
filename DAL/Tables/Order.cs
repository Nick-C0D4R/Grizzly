using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey("FK_OrderOffice")]
        public int OfficeId { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<Cart> Carts { get; set; } = new HashSet<Cart>();
    }
}
