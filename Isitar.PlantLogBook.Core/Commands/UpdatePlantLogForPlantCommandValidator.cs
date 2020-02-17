using System;
using System.Linq;
using FluentValidation;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Commands
{
    public class UpdatePlantLogForPlantCommandValidator : AbstractValidator<UpdatePlantLogForPlantCommand>
    {
        public UpdatePlantLogForPlantCommandValidator(PlantLogBookContext context)
        {
            RuleFor(x => x.Id)
                .Must(id => context.PlantLogs.Any(pl => pl.Id.Equals(id)))
                .WithMessage("Plant Log does not exist");
            
            RuleFor(x => x.PlantId)
                .Must((log, plantId) => context.PlantLogs.Any(pl => pl.Id.Equals(log.Id) && pl.PlantId.Equals(plantId)))
                .WithMessage("Plant Log does not exist");
         
            RuleFor(x => x.PlantLogTypeId)
                .Must(logTypeId => context.PlantLogTypes.Any(lt => lt.Id.Equals(logTypeId)))
                .WithMessage("Plant log type does not exist");
            

        }        
    }
}