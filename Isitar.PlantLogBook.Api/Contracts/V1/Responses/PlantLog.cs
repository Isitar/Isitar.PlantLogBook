using System;
using Isitar.PlantLogBook.Core.Responses;

namespace Isitar.PlantLogBook.Api.Contracts.V1.Responses
{
    public class PlantLog
    {
        public Guid Id { get; set; }
        
        public Guid PlantId { get; set; }

        public Guid PlantLogTypeId { get; set; }

        public DateTime DateTime { get; set; }
        public string Log { get; set; }


        public static PlantLog FromCore(PlantLogDto plantLog)
        {
            return new PlantLog
            {
                Id = plantLog.Id,
                PlantId = plantLog.PlantId,
                PlantLogTypeId = plantLog.PlantLogTypeId,
                DateTime = plantLog.DateTime,
                Log = plantLog.Log,
            };
        }
    }
}