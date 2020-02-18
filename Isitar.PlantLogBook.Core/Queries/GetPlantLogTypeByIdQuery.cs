using System;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Queries
{
    public class GetPlantLogTypeByIdQuery : IRequest<PlantLogTypeDtoResponse>
    {
        public Guid Id { get; set; }
    }
}