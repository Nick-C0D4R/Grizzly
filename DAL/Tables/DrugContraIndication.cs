using DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DAL.Tables
{
    public class DrugContraIndication : IContextTable
    {
        [Key]
        public int Id { get; set; }
        public string ContraIndication { get; set; }
    }
}
