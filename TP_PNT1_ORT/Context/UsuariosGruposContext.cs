using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP_PNT1_ORT.Models;

namespace TP_PNT1_ORT.Context
{
    public class UsuariosGruposContext : DbContext
    {
        public UsuariosGruposContext(DbContextOptions<UsuariosGruposContext> options)
           : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configura clave multiple
            modelBuilder.Entity<UsuarioGrupo>().HasKey(sc => new {  sc.email, sc.idGrupo });

            modelBuilder.Entity<UsuarioGrupo>()
                .HasOne<Usuario>(sc => sc.usuario)
                .WithMany(s => s.UsuariosGrupos)
                .HasForeignKey(sc => sc.email);

            modelBuilder.Entity<UsuarioGrupo>()
                .HasOne<Grupo>(sc => sc.grupo)
                .WithMany(s => s.UsuariosGrupos)
                .HasForeignKey(sc => sc.idGrupo);

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<UsuarioGrupo> UsuariosGrupos { get; set; }
        
        

    }
}
