using System.ComponentModel.DataAnnotations;

namespace CarlosCastaneda_PrimerLab_WebApi.Models
{
    public class roles
    {
        [Key]
        public int rolId { get; set; }
        public string rol { get; set; }
    }
}
