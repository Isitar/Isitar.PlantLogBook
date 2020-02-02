using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Data.DAO;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Handlers.CommandHandlers
{
    public class DeletePlantSpeciesCommandHandler : IRequestHandler<DeletePlantSpeciesCommand, Response>
    {
        private readonly PlantLogBookContext dbContext;

        public DeletePlantSpeciesCommandHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response> Handle(DeletePlantSpeciesCommand request, CancellationToken cancellationToken)
        {
            var plantSpecies = await dbContext.PlantSpecies.FindAsync(request.Id);
            if (null == plantSpecies)
            {
                var response = new Response();
                response.AddErrorMessage(nameof(request.Id), "No entity found");
                return response;
            }

            dbContext.PlantSpecies.Remove(plantSpecies);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new Response
            {
                Success = true
            };
        }
    }
}