using DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DAL.Tables
{
    public class DrugIndication : IContextTable
    {
        [Key]
        public int Id { get; set; }
        public string Indication { get; set; }
    }
}
