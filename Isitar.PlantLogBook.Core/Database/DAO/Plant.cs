using System;

namespace Isitar.PlantLogBook.Core.Database.DAO
{
    public class Plant
    {
        public Guid Id { get; set; }

        public Guid PlantSpeciesId { get; set; }
        public PlantSpecies PlantSpecies { get; set; }
        
    }
}