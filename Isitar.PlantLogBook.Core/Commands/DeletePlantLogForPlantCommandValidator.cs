using System;
using System.Linq;
using FluentValidation;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Commands
{
    public class DeletePlantLogForPlantCommandValidator : AbstractValidator<DeletePlantLogForPlantCommand>
    {
        public DeletePlantLogForPlantCommandValidator(PlantLogBookContext context)
        {
            RuleFor(x => x.Id)
                .Must(id => context.PlantLogs.Any(pl => pl.Id.Equals(id)))
                .WithMessage("Plant Log does not exist");
            
            RuleFor(x => x.PlantId)
                .Must((log, plantId) => context.PlantLogs.Any(pl => pl.Id.Equals(log.Id) && pl.PlantId.Equals(plantId)))
                .WithMessage("Plant Log does not exist");
        }        
    }
}