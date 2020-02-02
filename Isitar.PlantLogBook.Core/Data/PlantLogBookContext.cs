using Isitar.PlantLogBook.Core.Data.DAO;
using Microsoft.EntityFrameworkCore;

namespace Isitar.PlantLogBook.Core.Data
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