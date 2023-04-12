using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class DrugTypeDTO : IDTOEntity
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public ICollection<DrugDTO> Drugs { get; set; }
    }
}
