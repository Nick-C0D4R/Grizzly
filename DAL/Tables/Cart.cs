using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DAL.Tables
{
    public class Cart : ContextTable
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
