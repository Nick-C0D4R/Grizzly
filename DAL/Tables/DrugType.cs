﻿using DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Tables
{
    public class DrugType : ContextTable
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Drug> Drugs { get; set; } = new HashSet<Drug>();
    }
}
