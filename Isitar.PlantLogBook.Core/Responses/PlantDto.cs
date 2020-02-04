using System;
using System.Collections.Generic;
using Isitar.PlantLogBook.Core.Data.DAO;

namespace Isitar.PlantLogBook.Core.Responses
{
    public class PlantDtoResponse : Response<PlantDto>
    {
    }

    public class PlantDtosResponse : Response<IEnumerable<PlantDto>>
    {
    }

    public class PlantDto
    {
        public Guid Id { get; }
        public Guid PlantSpeciesId { get; }

        private PlantDto(Guid id, Guid plantSpeciesId)
        {
            Id = id;
            PlantSpeciesId = plantSpeciesId;
        }

        internal static PlantDto FromDao(Plant plant)
        {
            return new PlantDto(plant.Id, plant.PlantSpeciesId);
        }
    }
}