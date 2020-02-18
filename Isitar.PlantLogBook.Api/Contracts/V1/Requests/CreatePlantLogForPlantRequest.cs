using System;
namespace Isitar.PlantLogBook.Api.Contracts.V1.Requests
{
    public class CreatePlantLogForPlantRequest
    {
        public Guid PlantLogTypeId { get; set; }
        public DateTime DateTime { get; set; }
        public string Log { get; set; }
    }
}