using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Data.DAO;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Handlers.CommandHandlers
{
    public class CreatePlantLogForPlantCommandHandler : IRequestHandler<CreatePlantLogForPlantCommand, Response>
    {
        private PlantLogBookContext context;

        public CreatePlantLogForPlantCommandHandler(PlantLogBookContext context)
        {
            this.context = context;
        }

        public async Task<Response> Handle(CreatePlantLogForPlantCommand request, CancellationToken cancellationToken)
        {
            var plant = await context.Plants.FindAsync(request.PlantId);
            if (null == plant)
            {
                return new Response
                {
                    Success = false,
                    ErrorMessages =
                    {
                        {nameof(request.PlantId), new[] {"Plant not found"}}
                    },
                };
            }

            var log = new PlantLog
            {
                Id = request.Id,
                Plant = plant,
                PlantLogTypeId = request.PlantLogTypeId,
                DateTime = request.DateTime,
                Log = request.Log,
            };
            await context.PlantLogs.AddAsync(log);
            await context.SaveChangesAsync(cancellationToken);
            return new Response
            {
                Success = true
            };
        }
    }
}