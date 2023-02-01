using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class Drug
    {
        [Key]
        public int Id { get; set; }
        public string DrugName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        //[ForeignKey("FK_DrugProducer")]
        public int ProducerId { get; set; }

        public virtual Producer Producer { get; set; }
    }
}
