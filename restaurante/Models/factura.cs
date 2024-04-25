using System.ComponentModel.DataAnnotations;
namespace restaurante.Models
{
    public class factura
    {
        [Key]
        public int id_Factura { get; set; }
        public int id_Detalle { get; set; }
        public int id_Caja { get; set; }
    }
}
