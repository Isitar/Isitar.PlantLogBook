using System;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Commands
{
    public class DeletePlantLogTypeCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}