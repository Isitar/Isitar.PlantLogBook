using System;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Commands
{
    public class UpdatePlantLogCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
        public Guid PlantId { get; set; }
        public Guid PlantLogTypeId { get; set; }
        
        public DateTime DateTime { get; set; } = DateTime.Now;
        
        public string Log { get; set; }
        
    }
}