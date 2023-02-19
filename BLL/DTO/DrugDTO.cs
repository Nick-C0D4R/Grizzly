using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class DrugDTO
    {
        public int Id { get; set; }
        public string DrugName { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int ProducerId { get; set; }

        public override string ToString()
        {
            return $"Drug name: {DrugName} | Description: {Description} | Quantity: {Quantity} | " +
                $"Price: {Price} | Producer id: {ProducerId}";
        }

    }
}
