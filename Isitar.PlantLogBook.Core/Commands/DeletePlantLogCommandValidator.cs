using System;
using System.Linq;
using FluentValidation;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Commands
{
    public class DeletePlantLogCommandValidator : AbstractValidator<DeletePlantLogCommand>
    {
        public DeletePlantLogCommandValidator(PlantLogBookContext context)
        {
            RuleFor(x => x.Id)
                .Must(id => context.PlantLogs.Any(pl => pl.Id.Equals(id)));
        }        
    }
}