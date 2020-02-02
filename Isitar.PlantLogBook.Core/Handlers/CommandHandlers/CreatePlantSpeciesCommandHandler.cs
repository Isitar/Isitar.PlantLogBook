using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Data.DAO;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Handlers.CommandHandlers
{
    public class CreatePlantSpeciesCommandHandler : IRequestHandler<CreatePlantSpeciesCommand, Response>
    {
        private readonly PlantLogBookContext dbContext;

        public CreatePlantSpeciesCommandHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response> Handle(CreatePlantSpeciesCommand request, CancellationToken cancellationToken)
        {
            dbContext.PlantSpecies.Add(new PlantSpecies
            {
                Id = request.Id,
                Name = request.Name
            });
            await dbContext.SaveChangesAsync(cancellationToken);
            return new Response
            {
                Success = true
            };
        }
    }
}