﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Tables
{
    public class ActiveIngredient : ContextTable
    {
        [Key]
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public short Milligrams { get; set; }

        public virtual ICollection<Drug> Drugs { get; set; } = new HashSet<Drug>();
    }
}
