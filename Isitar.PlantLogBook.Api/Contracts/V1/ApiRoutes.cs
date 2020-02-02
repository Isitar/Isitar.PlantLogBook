namespace Isitar.PlantLogBook.Api.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";

        private const string Version = "v1";

        private const string Base = Root + "/" + Version;

        public static class PlantSpecies
        {
            private const string PlantSpeciesBase = Base + "/plant-species";
            public const string GetAll = PlantSpeciesBase;
            public const string Get = PlantSpeciesBase + "/{plantSpeciesId}";

            public const string Create = PlantSpeciesBase;
            public const string Update = PlantSpeciesBase + "/{plantSpeciesId}";
            public const string Delete = PlantSpeciesBase + "/{plantSpeciesId}";
        }
    }
}