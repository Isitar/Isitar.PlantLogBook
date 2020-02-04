namespace Isitar.PlantLogBook.Api.Contracts.V1
{
    /// <summary>
    /// Api routes for v1
    /// </summary>
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";

        private const string Base = Root + "/" + Version;

        /// <summary>
        /// Apis routes for plant species
        /// </summary>
        public static class PlantSpecies
        {
            private const string PlantSpeciesBase = Base + "/plant-species";
            public const string GetAll = PlantSpeciesBase;
            public const string Get = PlantSpeciesBase + "/{speciesId}";

            public const string Create = PlantSpeciesBase;
            public const string Update = PlantSpeciesBase + "/{speciesId}";
            public const string Delete = PlantSpeciesBase + "/{speciesId}";
        }

        /// <summary>
        /// Apis routes for plants
        /// </summary>
        public static class Plant
        {
            private const string PlantBase = Base + "/plant";
            public const string GetAll = PlantBase;
            public const string Get = PlantBase + "/{plantId}";

            public const string Create = PlantBase;
            public const string Update = PlantBase + "/{plantId}";
            public const string Delete = PlantBase + "/{plantId}";

            private const string PlantLogBase = PlantBase + "/{plantId}/plant-log";
            public const string GetAllPlantLogs = PlantLogBase;
            public const string GetPlantLog = PlantLogBase + "/{logId}";

            public const string CreatePlantLog = PlantLogBase;
            public const string UpdatePlantLog = PlantLogBase + "/{logId}";
            public const string DeletePlantLog = PlantLogBase + "/{logId}";
        }

        /// <summary>
        /// Apis routes for plant log types
        /// </summary>
        public static class PlantLogType
        {
            private const string PlantLogTypeBase = Base + "/plant-log-type";
            public const string GetAll = PlantLogTypeBase;
            public const string Get = PlantLogTypeBase + "/{logTypeId}";

            public const string Create = PlantLogTypeBase;
            public const string Update = PlantLogTypeBase + "/{logTypeId}";
            public const string Delete = PlantLogTypeBase + "/{logTypeId}";
        }
    }
}