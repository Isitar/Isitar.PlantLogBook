using System;
using System.Collections.Generic;
using Isitar.PlantLogBook.Core.Data.DAO;

namespace Isitar.PlantLogBook.Core.Responses
{
    public class PlantSpeciesDtoResponse : Response<PlantSpeciesDto>
    {
    }
    
    public class PlantSpeciesDtosResponse : Response<IEnumerable<PlantSpeciesDto>>
    {
    }

    public class PlantSpeciesDto
    {
        public Guid Id { get; }
        public string Name { get; }

        private PlantSpeciesDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        
        internal static PlantSpeciesDto FromDao(PlantSpecies plantSpecies)
        {
            return new PlantSpeciesDto(plantSpecies.Id, plantSpecies.Name);
        }
    }
}