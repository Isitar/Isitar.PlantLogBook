using System;

namespace Isitar.PlantLogBook.Core.Data.DAO
{
    internal class PlantLog
    {
        public Guid Id { get; set; }
        
        public Guid PlantId { get; set; }
        public Plant Plant { get; set; }

        public Guid PlantLogTypeId { get; set; }
        public PlantLogType PlantLogType { get; set; }

        public DateTime DateTime { get; set; }
        public string Log { get; set; }
    }
}