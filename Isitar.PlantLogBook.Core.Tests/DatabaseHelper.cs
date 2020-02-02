using Isitar.PlantLogBook.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Isitar.PlantLogBook.Core.Tests
{
    public class DatabaseHelper
    {
        public static PlantLogBookContext CreateInMemoryDatabaseContext(string name)
        {

            var options = new DbContextOptionsBuilder<PlantLogBookContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new PlantLogBookContext(options);
        }
    }
}