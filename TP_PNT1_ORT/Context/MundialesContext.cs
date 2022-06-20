using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP_PNT1_ORT.Models;

namespace TP_PNT1_ORT.Context
{
    public class MundialesContext : DbContext
    {
        public MundialesContext(DbContextOptions<MundialesContext> options)
           : base(options)
        {

        }
        
        public DbSet<Mundial> Mundiales { get; set; }
        
    }
}
