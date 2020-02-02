using System.Collections;
using System.Collections.Generic;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;

namespace Isitar.PlantLogBook.Core.Queries
{
    public class GetAllPlantSpeciesQuery : IRequest<PlantSpeciesDtosResponse>
    {
        public string NameFilter { get; set; }
    }
}