using System;
using System.Linq;
using FluentValidation;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Commands
{
    public class CreatePlantLogForPlantCommandValidator : AbstractValidator<CreatePlantLogForPlantCommand>
    {
        public CreatePlantLogForPlantCommandValidator(PlantLogBookContext context)
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.PlantLogTypeId)
                .Must(logTypeId => context.PlantLogTypes.Any(lt => lt.Id.Equals(logTypeId)))
                .WithMessage("Plant log type does not exist");
            RuleFor(x => x.PlantId)
                .Must(plantId => context.Plants.Any(p => p.Id.Equals(plantId)))
                .WithMessage("Plant does not exist");
        }        
    }
}