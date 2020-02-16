using System.Collections.Generic;
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
    public class GetAllPlantLogTypesQueryHandler : IRequestHandler<GetAllPlantLogTypesQuery, PlantLogTypeDtosResponse>
    {
        private readonly PlantLogBookContext dbContext;

        public GetAllPlantLogTypesQueryHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PlantLogTypeDtosResponse> Handle(GetAllPlantLogTypesQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.PlantLogTypes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.NameFilter))
            {
                query = query.Where(plt => plt.Name.Contains(request.NameFilter));    
            }

            var results = await query.Select(plt => PlantLogTypeDto.FromDao(plt)).ToListAsync(cancellationToken);
            return new PlantLogTypeDtosResponse
            {
                Success = true,
                Data = results,
            };

        }
    }
}