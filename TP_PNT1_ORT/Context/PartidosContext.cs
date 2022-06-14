using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP_PNT1_ORT.Models;

namespace TP_PNT1_ORT.Context
{
    public class PartidosContext : DbContext
    {
        public PartidosContext(DbContextOptions<PartidosContext> options)
           : base(options)
        {

        }

        
        public DbSet<Partido> partidos { get; set; }

    }
}
