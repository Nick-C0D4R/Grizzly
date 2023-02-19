﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FarmacyOfficeDTO
    {
        public int Id { get; set; }
        public string FarmacyTitle { get; set; }
        public string FarmacyAddress { get; set; }

        public override string ToString()
        {
            return $"Farmacy title: {FarmacyTitle} | Address: {FarmacyAddress}";
        }
    }
}
