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

        public DbSet<Grupo> grupos { get; set; }
    }
}
