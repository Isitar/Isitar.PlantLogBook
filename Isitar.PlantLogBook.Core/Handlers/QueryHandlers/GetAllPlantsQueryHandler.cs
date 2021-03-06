using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Data.DAO;
using Isitar.PlantLogBook.Core.Queries;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Isitar.PlantLogBook.Core.Handlers.QueryHandlers
{
    public class GetAllPlantsQueryHandler : IRequestHandler<GetAllPlantsQuery, PlantDtosResponse>
    {
        private readonly PlantLogBookContext dbContext;

        public GetAllPlantsQueryHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PlantDtosResponse> Handle(GetAllPlantsQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Plants.AsQueryable();

            var results = await query.Select(plant => PlantDto.FromDao(plant)).ToListAsync(cancellationToken: cancellationToken);
            return new PlantDtosResponse()
            {
                Success = true,
                Data = results,
            };
        }
    }
}