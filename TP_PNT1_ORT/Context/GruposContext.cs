using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP_PNT1_ORT.Models;

namespace TP_PNT1_ORT.Context
{
    public class GruposContext : DbContext
    {
        public GruposContext(DbContextOptions<GruposContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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

        public DbSet<UsuarioGrupo> UsuariosGrupos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Grupo> Grupos { get; set; }

        public DbSet<Mundial> Mundiales { get; set; }

    }
}
