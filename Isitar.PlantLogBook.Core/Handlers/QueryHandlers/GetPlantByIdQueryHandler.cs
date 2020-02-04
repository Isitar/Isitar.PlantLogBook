using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Queries;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Handlers.QueryHandlers
{
    public class GetPlantByIdQueryHandler : IRequestHandler<GetPlantByIdQuery, PlantDtoResponse>
    {
        private readonly PlantLogBookContext dbContext;

        public GetPlantByIdQueryHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PlantDtoResponse> Handle(GetPlantByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await dbContext.Plants.FindAsync(request.Id);
            if (null == result)
            {
                var response = new PlantDtoResponse();
                response.AddErrorMessage(nameof(request.Id), "No entity found");
                return response;
            }

            return new PlantDtoResponse
            {
                Success = true,
                Data = PlantDto.FromDao(result)
            };
        }
    }
}