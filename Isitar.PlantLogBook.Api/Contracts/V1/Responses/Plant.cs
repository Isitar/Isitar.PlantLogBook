using System;
using Isitar.PlantLogBook.Core.Responses;

namespace Isitar.PlantLogBook.Api.Contracts.V1.Responses
{
    public class Plant
    {
        public Guid Id { get; set; }
        public Guid PlantSpeciesId { get; set; }


        public static Plant FromCore(PlantDto plant)
        {
            return new Plant {Id = plant.Id, PlantSpeciesId = plant.PlantSpeciesId};
        }
    }
}