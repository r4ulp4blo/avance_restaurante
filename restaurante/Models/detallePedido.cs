using System.ComponentModel.DataAnnotations;
namespace restaurante.Models
{
    public class detallePedido
    {

        [Key]
        public int id_Detalle { get; set; }
        public DateOnly fecha { get; set; }
        public int id_Plato { get; set; }
        public int id_Mesa { get; set; }
        public string estado { get; set; }
        public int cantidadPlato { get; set; }
        public string? Comentarios { get; set; }
    }
}
