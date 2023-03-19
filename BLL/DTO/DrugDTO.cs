using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class DrugDTO : IDTOEntity
    {
        public int Id { get; set; }
        public string DrugName { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int ProducerId { get; set; }

        public int DrugTypeId { get; set; }
        
        public int ActiveIngredientId { get; set; }

        public ICollection<DrugIndicationDTO> Indications { get; set; } = new HashSet<DrugIndicationDTO>();
        public ICollection<DrugContraIndicationDTO> ContraIndications { get; set; } = new HashSet<DrugContraIndicationDTO>();
        public ICollection<DrugApplicationTypeDTO> ApplicationTypes { get; set; } = new HashSet<DrugApplicationTypeDTO>();

        public override string ToString()
        {
            string res = $"Drug name: {DrugName} | Description: {Description} | Quantity: {Quantity} | " +
                $"Price: {Price} | Producer id: {ProducerId} | Active ingredient: {ActiveIngredientId} | Drug type: {DrugTypeId} | ";

            res += "Indications : { ";
            foreach(DrugIndicationDTO item in Indications)
            {
                res += item.Indication;
            }
            res += " } | ";

            res += "Contraindications : { ";
            foreach (DrugContraIndicationDTO item in ContraIndications)
            {
                res += item.ContraIndication;
            }
            res += " } | ";

            res += "Application types : { ";
            foreach (DrugApplicationTypeDTO item in ApplicationTypes)
            {
                res += item.ApplicationType;
            }
            res += " }";

            return res;
        }

    }
}
