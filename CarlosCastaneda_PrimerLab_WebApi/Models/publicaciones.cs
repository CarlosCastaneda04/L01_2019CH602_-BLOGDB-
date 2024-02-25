using System.ComponentModel.DataAnnotations;

namespace CarlosCastaneda_PrimerLab_WebApi.Models
{
    public class publicaciones
    {
        [Key]
        public int publicacionId { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int usuarioId { get; set; }
    }

}
