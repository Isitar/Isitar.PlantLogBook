using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Data.DAO;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Handlers.CommandHandlers
{
    public class DeletePlantCommandHandler : IRequestHandler<DeletePlantCommand, Response>
    {
        private readonly PlantLogBookContext dbContext;

        public DeletePlantCommandHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response> Handle(DeletePlantCommand request, CancellationToken cancellationToken)
        {
            var plant = await dbContext.Plants.FindAsync(request.Id);
            if (null == plant)
            {
                var response = new Response();
                response.AddErrorMessage(nameof(request.Id), "No entity found");
                return response;
            }

            dbContext.Plants.Remove(plant);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new Response
            {
                Success = true
            };
        }
    }
}