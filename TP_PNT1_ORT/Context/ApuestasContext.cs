using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP_PNT1_ORT.Models;

namespace TP_PNT1_ORT.Context
{
    public class ApuestasContext : DbContext
    {
        public ApuestasContext(DbContextOptions<ApuestasContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Apuesta>()
                .HasKey(a => new { a.idGrupo, a.email, a.idPartido });


            modelBuilder.Entity<UsuarioGrupo>()
                .HasKey(sc => new { sc.email, sc.idGrupo });

            modelBuilder.Entity<UsuarioGrupo>()
               .HasOne<Usuario>(sc => sc.usuario)
               .WithMany(s => s.UsuariosGrupos)
               .HasForeignKey(sc => sc.email);

            modelBuilder.Entity<UsuarioGrupo>()
                .HasOne<Grupo>(sc => sc.grupo)
                .WithMany(s => s.UsuariosGrupos)
                .HasForeignKey(sc => sc.idGrupo);

        }

        public DbSet<Apuesta> apuestas { get; set; }
        public DbSet<Partido> partidos { get; set; }
        public DbSet<Grupo> grupos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<UsuarioGrupo> UsuariosGrupos { get; set; }

    }
}
