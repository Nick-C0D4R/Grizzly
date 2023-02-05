using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public string UserLogin { get; set; }

        public DateTime OrderDate { get; set; }

        //[ForeignKey("Order")]
        //public int OrderId { get; set; }


        public virtual ICollection<Drug> Drugs { get; set; } = new HashSet<Drug>();
        //public virtual Order Order { get; set; }
    }
}
