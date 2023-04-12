using DAL.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Tables
{
    public class Order : ContextTable
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
