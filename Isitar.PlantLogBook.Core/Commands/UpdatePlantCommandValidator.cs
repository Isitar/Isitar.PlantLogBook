using System.Linq;
using FluentValidation;
using Isitar.PlantLogBook.Core.Data;

namespace Isitar.PlantLogBook.Core.Commands
{
    public class UpdatePlantCommandValidator : AbstractValidator<UpdatePlantCommand>
    {
        public UpdatePlantCommandValidator(PlantLogBookContext dbContext)
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Id)
                .Must(plantId => dbContext.Plants.Any(ps => ps.Id.Equals(plantId)))
                .WithMessage("Plant does not exist");
            RuleFor(p => p.PlantSpeciesId)
                .Must(plantSpeciesId => dbContext.PlantSpecies.Any(ps => ps.Id.Equals(plantSpeciesId)))
                .WithMessage("Plant species does not exist");
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}