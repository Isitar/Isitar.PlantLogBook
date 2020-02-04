using System;

namespace Isitar.PlantLogBook.Api.Contracts.V1.Requests
{
    public class CreatePlantRequest
    {
        public Guid PlantSpeciesId { get; set; }
    }
}