using System;

namespace Isitar.PlantLogBook.Core.Data.DAO
{
    internal class Plant
    {
        public Guid Id { get; set; }

        public Guid PlantSpeciesId { get; set; }
        public PlantSpecies PlantSpecies { get; set; }
        
    }
}