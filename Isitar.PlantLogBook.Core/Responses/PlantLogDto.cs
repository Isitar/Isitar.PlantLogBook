using System;
using System.Collections.Generic;
using Isitar.PlantLogBook.Core.Data.DAO;

namespace Isitar.PlantLogBook.Core.Responses
{
    public class PlantLogDtoResponse : Response<PlantLogDto>
    {
    }

    public class PlantLogDtosResponse : Response<IEnumerable<PlantLogDto>>
    {
    }

    public class PlantLogDto
    {
        public Guid Id { get; set; }
        
        public Guid PlantId { get; set; }

        public Guid PlantLogTypeId { get; set; }

        public DateTime DateTime { get; set; }
        public string Log { get; set; }

        private PlantLogDto(Guid id, Guid plantId, Guid plantLogTypeId, DateTime dateTime, string log)
        {
            Id = id;
            PlantId = plantId;
            PlantLogTypeId = plantLogTypeId;
            DateTime = dateTime;
            Log = log;
        }

        internal static PlantLogDto FromDao(PlantLog plantLog)
        {
            return new PlantLogDto(plantLog.Id, plantLog.PlantId, plantLog.PlantLogTypeId, plantLog.DateTime, plantLog.Log);
        }
    }
}