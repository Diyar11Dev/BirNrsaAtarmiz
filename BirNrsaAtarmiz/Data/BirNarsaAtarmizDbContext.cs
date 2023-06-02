using BirNrsaAtarmiz.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BirNrsaAtarmiz.Data
{
    public class BirNarsaAtarmizDbContext : DbContext
    {
        public BirNarsaAtarmizDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }  
    }
}
