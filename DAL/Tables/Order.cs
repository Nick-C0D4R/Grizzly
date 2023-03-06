using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class Order : IContextTable
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        #region Roreign Keys
        [ForeignKey("Office")]
        public int OfficeId { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        #endregion

        #region Virtual Properties
        public virtual Cart Cart { get; set; }
        public virtual FarmacyOffice Office { get; set; }
        #endregion 
    }
}
