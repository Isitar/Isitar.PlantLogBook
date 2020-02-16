using System;
namespace Isitar.PlantLogBook.Api.Contracts.V1.Requests
{
    public class GetAllPlantLogsRequest
    { 
        public Guid[] PlantLogTypes { get; set; }

        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }

        public string LogFilter { get; set; }
    }
}