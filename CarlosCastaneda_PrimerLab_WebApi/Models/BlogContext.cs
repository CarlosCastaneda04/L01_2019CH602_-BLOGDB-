using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace CarlosCastaneda_PrimerLab_WebApi.Models
{
    public class BlogContext : DbContext
    {

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {


        }

        public DbSet<calificaciones> calificaciones { get; set; }
        public DbSet<comments> comentarios { get; set; }
        public DbSet<publicaciones> publicaciones { get; set; }
        public DbSet<roles> roles { get; set; }
        public DbSet<usuarios> usuarios { get; set; }

    }
}
