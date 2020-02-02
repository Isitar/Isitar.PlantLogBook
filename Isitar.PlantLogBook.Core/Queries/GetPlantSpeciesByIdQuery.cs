using System;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Queries
{
    public class GetPlantSpeciesByIdQuery : IRequest<PlantSpeciesDtoResponse>
    {
        public Guid Id { get; set; }
    }
}