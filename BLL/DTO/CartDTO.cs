using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CartDTO : IDTOEntity
    {
        public int Id { get; set; }

        public string UserLogin { get; set; }

        public DateTime OrderDate { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} | User login: {UserLogin} | Order date: {OrderDate}";
        }
    }
}
