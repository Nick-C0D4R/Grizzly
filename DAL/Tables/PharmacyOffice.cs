using System.ComponentModel.DataAnnotations;

namespace DAL.Tables
{
    public class PharmacyOffice : ContextTable
    {
        [Key]
        public int Id { get; set; }
        public string FarmacyTitle { get; set; }
        public string FarmacyAddress { get; set; }
    }
}
