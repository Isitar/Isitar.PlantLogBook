using System;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Queries
{
    public class GetAllPlantLogsQuery : IRequest<PlantLogDtosResponse>
    {
        private string logFilter = string.Empty;
        public Guid PlantId { get; set; } = Guid.Empty;
        
        public Guid[] PlantLogTypes { get; set; } = new Guid[0];

        public DateTime FromDateTime { get; set; } = DateTime.MinValue;
        public DateTime ToDateTime { get; set; } = DateTime.MaxValue;

        public string LogFilter
        {
            get => logFilter;
            set => logFilter = value.Trim();
        }
    }
}