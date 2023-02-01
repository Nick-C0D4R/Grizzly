using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class FarmacyOfficeRepository
    {
        private FarmacyContext _context;

        public FarmacyOfficeRepository(FarmacyContext context)
        {
            _context = context;
        }
    }
}
