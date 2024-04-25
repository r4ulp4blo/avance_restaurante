using System.ComponentModel.DataAnnotations;
namespace restaurante.Models
{
    public class caja
    {
        [Key]
        public int id_Caja { get; set; }
        public string nombreCobrador { get; set; }
        public int id_Mesa { get; set; }
        public decimal totalCobro { get; set; }
    }
}
