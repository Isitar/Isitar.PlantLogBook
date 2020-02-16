using System;
using Isitar.PlantLogBook.Core.Data.DAO;

namespace Isitar.PlantLogBook.Core.Tests.Helpers
{
    class ValidObjectHelper
    {
        private static Random rnd = new Random();

        public static PlantSpecies ValidPlantSpecies()
        {
            var name =
                $"{NameList.FirstNames[rnd.Next(NameList.FirstNames.Length)]} {NameList.Names[rnd.Next(NameList.Names.Length)]}";
            return new PlantSpecies {Id = Guid.NewGuid(), Name = name};
        }

        public static Plant ValidPlant(PlantSpecies plantSpecies)
        {
            var name =
                $"{NameList.FirstNames[rnd.Next(NameList.FirstNames.Length)]} {NameList.Names[rnd.Next(NameList.Names.Length)]}";
            return new Plant
                {Id = Guid.NewGuid(), Name = name, PlantSpeciesId = plantSpecies.Id, PlantState = PlantState.Active};
        }

        public static PlantLog ValidPlantLog(Plant plant, PlantLogType plantLogType)
        {
            var log = $"{NameList.FirstNames[rnd.Next(NameList.FirstNames.Length)]}: {LoremIpsum.Words(25)}";
            return new PlantLog {Id = Guid.NewGuid(), PlantId = plant.Id, DateTime = DateTime.Now, Log = log, PlantLogTypeId = plantLogType.Id};
        }

        public static PlantLogType ValidPlantLogType()
        {
            var name =
                $"{NameList.FirstNames[rnd.Next(NameList.FirstNames.Length)]} {NameList.Names[rnd.Next(NameList.Names.Length)]}";
            return new PlantLogType {Id = Guid.NewGuid(), Name = name};
        }
    }
}