using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Queries;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Handlers.QueryHandlers
{
    public class GetPlantSpeciesByIdQueryHandler : IRequestHandler<GetPlantSpeciesByIdQuery, PlantSpeciesDtoResponse>
    {
        private readonly PlantLogBookContext dbContext;

        public GetPlantSpeciesByIdQueryHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PlantSpeciesDtoResponse> Handle(GetPlantSpeciesByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await dbContext.PlantSpecies.FindAsync(request.Id);
            if (null == result)
            {
                var response = new PlantSpeciesDtoResponse();
                response.AddErrorMessage(nameof(request.Id), "No entity found");
                return response;
            }
            return new PlantSpeciesDtoResponse
            {
                Success = true,
                Data = PlantSpeciesDto.FromDao(result)
            };
        }
    }
}