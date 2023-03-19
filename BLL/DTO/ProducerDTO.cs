using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ProducerDTO : IDTOEntity
    {
        public int Id { get; set; }
        public string ProducerName { get; set; }

        public override string ToString()
        {
            return ProducerName;
        }
    }
}
