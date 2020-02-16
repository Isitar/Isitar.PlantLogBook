using System;
using System.Collections.Generic;
using Isitar.PlantLogBook.Core.Data.DAO;

namespace Isitar.PlantLogBook.Core.Responses
{
    public class PlantLogTypeDtoResponse : Response<PlantLogTypeDto>
    {
    }

    public class PlantLogTypeDtosResponse : Response<IEnumerable<PlantLogTypeDto>>
    {
    }

    public class PlantLogTypeDto
    {
        public Guid Id { get; }
        public string Name { get; }

        private PlantLogTypeDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        internal static PlantLogTypeDto FromDao(PlantLogType plantLogType)
        {
            return new PlantLogTypeDto(plantLogType.Id, plantLogType.Name);
        }
    }
}