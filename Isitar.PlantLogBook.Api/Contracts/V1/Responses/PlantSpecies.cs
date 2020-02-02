using System;
using Isitar.PlantLogBook.Core.Responses;

namespace Isitar.PlantLogBook.Api.Contracts.V1.Responses
{
    /// <summary>
    /// A plant species
    /// A species is a biological designation for any life form that identifies it within an established
    /// ranking system based on its physical and genetic similarities to other life forms.
    /// In botany, plants are known by their scientific name, using a system known as binomial nomenclature.
    /// </summary>
    public class PlantSpecies
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static PlantSpecies FromCore(PlantSpeciesDto plantSpeciesDto)
        {
            return new PlantSpecies {Id = plantSpeciesDto.Id, Name = plantSpeciesDto.Name};
        }
    }
}