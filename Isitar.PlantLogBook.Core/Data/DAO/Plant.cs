using System;
using System.Collections.Generic;

namespace Isitar.PlantLogBook.Core.Data.DAO
{
    internal enum PlantState
    {
        Active,
        Inactive
    }

    internal class Plant
    {
        public Guid Id { get; set; }

        public Guid PlantSpeciesId { get; set; }
        public PlantSpecies PlantSpecies { get; set; }


        public string Name { get; set; }
        public PlantState PlantState { get; set; }

        public IEnumerable<PlantLog> PlantLogs { get; set; }
    }
}