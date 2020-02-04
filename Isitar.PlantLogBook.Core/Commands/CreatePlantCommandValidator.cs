using System.Linq;
using FluentValidation;
using Isitar.PlantLogBook.Core.Data;

namespace Isitar.PlantLogBook.Core.Commands
{
    public class CreatePlantCommandValidator : AbstractValidator<CreatePlantCommand>
    {
        public CreatePlantCommandValidator(PlantLogBookContext dbContext)
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.PlantSpeciesId)
                .Must(plantSpeciesId => dbContext.PlantSpecies.Any(ps => ps.Id.Equals(plantSpeciesId)))
                .WithMessage("Plant species does not exist");
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}