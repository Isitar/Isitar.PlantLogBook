using System;
using Isitar.PlantLogBook.Core.Responses;

namespace Isitar.PlantLogBook.Api.Contracts.V1.Responses
{
    /// <summary>
    /// A plant log type
    /// </summary>
    public class PlantLogType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static PlantLogType FromCore(PlantLogTypeDto plantLogTypeDto)
        {
            return new PlantLogType {Id = plantLogTypeDto.Id, Name = plantLogTypeDto.Name};
        }
    }
}