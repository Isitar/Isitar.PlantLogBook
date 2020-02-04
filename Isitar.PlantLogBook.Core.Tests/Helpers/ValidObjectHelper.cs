using System;
using Isitar.PlantLogBook.Core.Data.DAO;

namespace Isitar.PlantLogBook.Core.Tests
{
    public class ValidObjectHelper
    {
        public const names = [
        ]
        
        Random rnd = new Random();
        public static PlantSpecies ValidPlantSpecies()
        {
            
            return new PlantSpecies {Id = Guid.NewGuid(), Name = };
        }
    }
}