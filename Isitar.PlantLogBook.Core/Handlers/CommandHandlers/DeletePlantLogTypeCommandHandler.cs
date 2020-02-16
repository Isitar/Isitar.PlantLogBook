using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Data.DAO;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Handlers.CommandHandlers
{
    public class DeletePlantLogTypeCommandHandler : IRequestHandler<DeletePlantLogTypeCommand, Response>
    {
        private readonly PlantLogBookContext dbContext;

        public DeletePlantLogTypeCommandHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response> Handle(DeletePlantLogTypeCommand request, CancellationToken cancellationToken)
        {
            var plantLogType = await dbContext.PlantLogTypes.FindAsync(request.Id);
            if (null == plantLogType)
            {
                var response = new Response();
                response.AddErrorMessage(nameof(request.Id), "No entity found");
                return response;
            }

            dbContext.PlantLogTypes.Remove(plantLogType);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new Response
            {
                Success = true
            };
        }
    }
}