using DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DAL.Tables
{
    public class FarmacyOffice : ContextTable
    {
        [Key]
        public int Id { get; set; }
        public string FarmacyTitle { get; set; }
        public string FarmacyAddress { get; set; }
    }
}
