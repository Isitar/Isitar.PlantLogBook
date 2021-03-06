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
    public class GetAllPlantSpeciesQueryHandler : IRequestHandler<GetAllPlantSpeciesQuery, PlantSpeciesDtosResponse>
    {
        private readonly PlantLogBookContext dbContext;

        public GetAllPlantSpeciesQueryHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PlantSpeciesDtosResponse> Handle(GetAllPlantSpeciesQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.PlantSpecies.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.NameFilter))
            {
                query = query.Where(ps => ps.Name.Contains(request.NameFilter));    
            }

            var results = await query.Select(ps => PlantSpeciesDto.FromDao(ps)).ToListAsync(cancellationToken);
            return new PlantSpeciesDtosResponse
            {
                Success = true,
                Data = results,
            };

        }
    }
}