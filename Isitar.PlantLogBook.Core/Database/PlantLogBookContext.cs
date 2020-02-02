using Isitar.PlantLogBook.Core.Database.DAO;
using Microsoft.EntityFrameworkCore;

namespace Isitar.PlantLogBook.Core.Database
{
    public class PlantLogBookContext : DbContext
    {
        public PlantLogBookContext(DbContextOptions<PlantLogBookContext> options) : base(options)
        {
        }

        internal virtual DbSet<PlantSpecies> PlantSpecies { get; set; }
        internal virtual DbSet<Plant> Plants { get; set; }
        internal virtual DbSet<PlantLogType> PlantLogTypes { get; set; }
        internal virtual DbSet<PlantLog> PlantLogs { get; set; }
    }
}