using DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DAL.Tables
{
    public class ActiveIngredient : IContextTable
    {
        [Key]
        public int Id { get; set; }
        public string IngredientName { get; set; }
    }
}
