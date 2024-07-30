using Microsoft.EntityFrameworkCore;
using PokeApiDemo.Models;

namespace PokeApiDemo.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Master> Masters { get; set; }
        public DbSet<CapturedPokemon> CapturedPokemons { get; set; }
    }
}
