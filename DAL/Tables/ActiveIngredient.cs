﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class ActiveIngredient : IContextTable
    {
        [Key]
        public int Id { get; set; }
        public string IngredientName { get; set; }
    }
}
