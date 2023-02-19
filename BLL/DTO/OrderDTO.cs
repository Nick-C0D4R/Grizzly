using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CartId { get; set; }

        public override string ToString()
        {
            return $"Office id: {OfficeId} | Cart id: {CartId} | Ordered: {OrderDate}";
        }
    }
}
