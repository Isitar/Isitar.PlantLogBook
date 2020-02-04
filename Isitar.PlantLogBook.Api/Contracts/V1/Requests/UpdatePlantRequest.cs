using System;

namespace Isitar.PlantLogBook.Api.Contracts.V1.Requests
{
    public class UpdatePlantRequest
    {
        public Guid PlantSpeciesId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}