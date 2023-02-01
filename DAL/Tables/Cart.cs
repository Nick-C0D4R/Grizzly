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

        //[ForeignKey("FK_CartDrug")]
        public int DrugId { get; set; }


        public virtual Drug Drug { get; set; }
        public virtual Order Order { get; set; }
    }
}
