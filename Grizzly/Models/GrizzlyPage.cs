using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Grizzly.Models
{
    public class GrizzlyPage : Page
    {
        public GrizzlyPage(GrizzlyPage child = null)
        {
            Child = child;
        }

        public GrizzlyPage Child { get; set; } = null;
    }
}
