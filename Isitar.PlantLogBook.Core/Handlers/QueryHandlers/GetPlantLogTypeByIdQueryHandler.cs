using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Queries;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Handlers.QueryHandlers
{
    public class GetPlantLogTypeByIdQueryHandler : IRequestHandler<GetPlantLogTypeByIdQuery, PlantLogTypeDtoResponse>
    {
        private readonly PlantLogBookContext dbContext;

        public GetPlantLogTypeByIdQueryHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PlantLogTypeDtoResponse> Handle(GetPlantLogTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await dbContext.PlantLogTypes.FindAsync(request.Id);
            if (null == result)
            {
                var response = new PlantLogTypeDtoResponse();
                response.AddErrorMessage(nameof(request.Id), "No entity found");
                return response;
            }
            return new PlantLogTypeDtoResponse
            {
                Success = true,
                Data = PlantLogTypeDto.FromDao(result)
            };
        }
    }
}