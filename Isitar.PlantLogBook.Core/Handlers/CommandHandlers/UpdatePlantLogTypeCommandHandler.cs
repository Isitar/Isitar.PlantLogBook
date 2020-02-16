using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Handlers.CommandHandlers
{
    public class UpdatePlantLogTypeCommandHandler : IRequestHandler<UpdatePlantLogTypeCommand, Response>
    {
        private readonly PlantLogBookContext dbContext;

        public UpdatePlantLogTypeCommandHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response> Handle(UpdatePlantLogTypeCommand request, CancellationToken cancellationToken)
        {
            var plantLogType = await dbContext.PlantLogTypes.FindAsync(request.Id);
            if (null == plantLogType)
            {
                var retVal = new Response();
                retVal.AddErrorMessage(nameof(request.Id), "No entity found");
                return retVal;
            }
            plantLogType.Name = request.Name;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new Response {Success = true};
        }
    }
}