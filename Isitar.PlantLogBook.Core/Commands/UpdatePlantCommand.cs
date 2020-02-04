using System;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Commands
{
    public class UpdatePlantCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
        public Guid PlantSpeciesId { get; set; }
    }
}