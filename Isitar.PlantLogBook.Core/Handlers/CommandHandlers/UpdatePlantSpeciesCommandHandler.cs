using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Handlers.CommandHandlers
{
    public class UpdatePlantSpeciesCommandHandler : IRequestHandler<UpdatePlantSpeciesCommand, Response>
    {
        private readonly PlantLogBookContext dbContext;

        public UpdatePlantSpeciesCommandHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response> Handle(UpdatePlantSpeciesCommand request, CancellationToken cancellationToken)
        {
            var plantSpecies = await dbContext.PlantSpecies.FindAsync(request.Id);
            if (null == plantSpecies)
            {
                var retVal = new Response();
                retVal.AddErrorMessage(nameof(request.Id), "No entity found");
                return retVal;
            }
            plantSpecies.Name = request.Name;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new Response {Success = true};
        }
    }
}