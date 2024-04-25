using System.ComponentModel.DataAnnotations;

namespace restaurante.Models
{
    public class plato
    {
        [Key]
        [Display(Name = "id de plato")]
        public int id_Plato { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Precio")]
        public decimal precio { get; set; }
        
    }
}
