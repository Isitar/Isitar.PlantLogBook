using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Data.DAO;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Handlers.CommandHandlers
{
    public class UpdatePlantCommandHandler : IRequestHandler<UpdatePlantCommand, Response>
    {
        private readonly PlantLogBookContext dbContext;

        public UpdatePlantCommandHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response> Handle(UpdatePlantCommand request, CancellationToken cancellationToken)
        {
            var plant = await dbContext.Plants.FindAsync(request.Id);
            if (null == plant)
            {
                var retVal = new Response();
                retVal.AddErrorMessage(nameof(request.Id), "No entity found");
                return retVal;
            }

            plant.PlantSpeciesId = request.PlantSpeciesId;
            plant.Name = request.Name;
            plant.PlantState = request.IsActive ? PlantState.Active : PlantState.Inactive;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new Response {Success = true};
        }
    }
}