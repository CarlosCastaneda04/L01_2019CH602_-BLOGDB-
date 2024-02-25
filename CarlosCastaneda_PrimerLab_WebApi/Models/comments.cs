using System.ComponentModel.DataAnnotations;

namespace CarlosCastaneda_PrimerLab_WebApi.Models
{
    public class comments
    {
        [Key]
        public int cometarioId { get; set; }
        public int? publicacionId { get; set; }
        public string comentario { get; set; }
        public int? usuarioId { get; set; }
    }
}
