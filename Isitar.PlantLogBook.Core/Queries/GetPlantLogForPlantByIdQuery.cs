using System;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Queries
{
    public class GetPlantLogForPlantByIdQuery : IRequest<PlantLogDtoResponse>
    {
        public Guid Id { get; set; }
        public Guid PlantId { get; set; }
    }
}