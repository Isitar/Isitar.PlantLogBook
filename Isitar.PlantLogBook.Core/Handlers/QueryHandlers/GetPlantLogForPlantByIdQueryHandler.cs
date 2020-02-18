using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Queries;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Isitar.PlantLogBook.Core.Handlers.QueryHandlers
{
    public class GetPlantLogForPlantByIdQueryHandler : IRequestHandler<GetPlantLogForPlantByIdQuery, PlantLogDtoResponse>
    {
        private PlantLogBookContext context;

        public GetPlantLogForPlantByIdQueryHandler(PlantLogBookContext context)
        {
            this.context = context;
        }

        public async Task<PlantLogDtoResponse> Handle(GetPlantLogForPlantByIdQuery request,
            CancellationToken cancellationToken)
        {
            var plantLog = await context
                .Plants
                .Include(p => p.PlantLogs)
                .Where(p => p.Id.Equals(request.PlantId))
                .SelectMany(p => p.PlantLogs)
                .Where(pl => pl.Id.Equals(request.Id))
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);
            if (null == plantLog)
            {
                return new PlantLogDtoResponse
                {
                    Success = false,
                    ErrorMessages =
                    {
                        {nameof(request.Id), new[] {"No entity found"}}
                    }
                };
            }

            return new PlantLogDtoResponse
            {
                Success = true,
                Data = PlantLogDto.FromDao(plantLog),
            };
        }
    }
}