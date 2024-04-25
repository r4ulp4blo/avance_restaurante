using System.ComponentModel.DataAnnotations;
namespace restaurante.Models
{
    public class mesa
    {
        [Key]
        public int id_Mesa { get; set; }
        public string tipo { get; set; }
        public string estado { get; set; }
    }
}
