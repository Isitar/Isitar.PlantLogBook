using System;

namespace Isitar.PlantLogBook.Core.Database.DAO
{
    public class PlantLog
    {
        public Guid Id { get; set; }
        
        public Guid PlantId { get; set; }
        public Plant Plant { get; set; }

        public Guid PlantLogTypeId { get; set; }
        public PlantLogType LogType { get; set; }

        public DateTime DateTime { get; set; }
        public string Log { get; set; }
    }
}