using System;
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
    public class GetAllPlantLogsQueryHandler : IRequestHandler<GetAllPlantLogsQuery, PlantLogDtosResponse>
    {
        private readonly PlantLogBookContext dbContext;

        public GetAllPlantLogsQueryHandler(PlantLogBookContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PlantLogDtosResponse> Handle(GetAllPlantLogsQuery request,
            CancellationToken cancellationToken)
        {
            var query = dbContext.PlantLogs.AsQueryable();
            if (Guid.Empty != request.PlantId)
            {
                query = query.Where(pl => pl.PlantId == request.PlantId);
            }

            if (request.PlantLogTypes.Length > 0)
            {
                query = query.Where(pl => request.PlantLogTypes.Contains(pl.PlantLogTypeId));
            }

            if (!string.IsNullOrWhiteSpace(request.LogFilter))
            {
                query = query.Where(pl => pl.Log.Contains(request.LogFilter));
            }

            query = query.Where(pl => pl.DateTime >= request.FromDateTime && pl.DateTime <= request.ToDateTime);

            var results = await query.Select(ps => PlantLogDto.FromDao(ps))
                .ToListAsync(cancellationToken: cancellationToken);
            return new PlantLogDtosResponse()
            {
                Success = true,
                Data = results,
            };
        }
    }
}