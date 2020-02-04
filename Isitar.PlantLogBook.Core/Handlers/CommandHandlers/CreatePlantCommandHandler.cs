using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Data.DAO;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Handlers.CommandHandlers
{
    public class CreatePlantCommandHandler : IRequestHandler<CreatePlantCommand, Response>
    {
        private readonly PlantLogBookContext dbContext;

        public CreatePlantCommandHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response> Handle(CreatePlantCommand request, CancellationToken cancellationToken)
        {
            dbContext.Plants.Add(new Plant
            {
                Id = request.Id,
                PlantSpeciesId = request.PlantSpeciesId,
                Name = request.Name,
                PlantState = PlantState.Active,
            });
            await dbContext.SaveChangesAsync(cancellationToken);
            return new Response
            {
                Success = true
            };
        }
    }
}