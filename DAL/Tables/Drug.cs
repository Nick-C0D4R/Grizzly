using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Tables
{
    public class Drug : IContextTable
    {
        [Key]
        public int Id { get; set; }
        public string DrugName { get; set; }
        public string Description { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        #region Foreign Keys

        [ForeignKey("DrugType")]
        public int DrugTypeId { get; set; }

        [ForeignKey("ActiveIngredient")]
        public int ActiveIngredientId { get; set; }

        [ForeignKey("Producer")]
        public int ProducerId { get; set; }
        
        #endregion

        #region Virtual Properties

        public virtual Producer Producer { get; set; }
        public virtual DrugType DrugType { get; set; }
        public virtual ActiveIngredient ActiveIngredient { get; set; }
        public virtual ICollection<DrugIndication> Indications { get; set; } = new HashSet<DrugIndication>();
        public virtual ICollection<DrugContraIndication> ContraIndications { get; set; } = new HashSet<DrugContraIndication>();
        public virtual ICollection<DrugApplicationType> ApplicationTypes { get; set; } = new HashSet<DrugApplicationType>();
        #endregion
    }
}
