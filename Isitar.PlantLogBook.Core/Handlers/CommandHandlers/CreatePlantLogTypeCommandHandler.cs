using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Data.DAO;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Handlers.CommandHandlers
{
    public class CreatePlantLogTypeCommandHandler : IRequestHandler<CreatePlantLogTypeCommand, Response>
    {
        private readonly PlantLogBookContext dbContext;

        public CreatePlantLogTypeCommandHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response> Handle(CreatePlantLogTypeCommand request, CancellationToken cancellationToken)
        {
            dbContext.PlantLogTypes.Add(new PlantLogType
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