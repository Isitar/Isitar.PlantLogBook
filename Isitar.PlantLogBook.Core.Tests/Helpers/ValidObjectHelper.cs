using System;
using Isitar.PlantLogBook.Core.Data.DAO;

namespace Isitar.PlantLogBook.Core.Tests.Helpers
{
    class ValidObjectHelper
    {
        
        private static Random rnd = new Random();
        public static PlantSpecies ValidPlantSpecies()
        {
            var name =$"{NameList.FirstNames[rnd.Next(NameList.FirstNames.Length)]} {NameList.Names[rnd.Next(NameList.Names.Length)]}";
            return new PlantSpecies {Id = Guid.NewGuid(), Name = name};
        }
    }
}