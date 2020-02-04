using System;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Queries
{
    public class GetPlantByIdQuery : IRequest<PlantDtoResponse>
    {
        public Guid Id { get; set; }
    }
}