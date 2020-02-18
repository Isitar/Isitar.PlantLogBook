using System;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Queries
{
    public class GetPlantLogByIdQuery : IRequest<PlantLogDtoResponse>
    {
        public Guid Id { get; set; }
    }
}