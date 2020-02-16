using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Data.DAO;
using Isitar.PlantLogBook.Core.Handlers.CommandHandlers;
using Isitar.PlantLogBook.Core.Handlers.QueryHandlers;
using Isitar.PlantLogBook.Core.Queries;
using Isitar.PlantLogBook.Core.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Isitar.PlantLogBook.Core.Tests
{
    public class PlantCrudTests
    {
        [Fact]
        public async Task CreatePlant()
        {
            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(CreatePlant));

            // create plant species
            var plantSpecies1 = ValidObjectHelper.ValidPlantSpecies();
            await context.PlantSpecies.AddRangeAsync(
                plantSpecies1
            );
            await context.SaveChangesAsync();

            var validPlant = ValidObjectHelper.ValidPlant(plantSpecies1);
            var id = validPlant.Id;
            var name = validPlant.Name;
            var cntBefore = await context.Plants.CountAsync();

            var createCmd = new CreatePlantCommand {Id = id, Name = name, PlantSpeciesId = plantSpecies1.Id};
            var cmdHandler = new CreatePlantCommandHandler(context);
            var result = await cmdHandler.Handle(createCmd, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(cntBefore + 1, await context.Plants.CountAsync());
            var obj = await context.Plants.FindAsync(id);
            Assert.Equal(id, obj.Id);
            Assert.Equal(name, obj.Name);
            Assert.Equal(plantSpecies1.Id, obj.PlantSpeciesId);
        }

        [Fact]
        public async Task UpdatePlant()
        {
            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(UpdatePlant));
            var plantSpecies1 = ValidObjectHelper.ValidPlantSpecies();
            await context.PlantSpecies.AddAsync(
                plantSpecies1
            );
            var validPlant = ValidObjectHelper.ValidPlant(plantSpecies1);
            await context.Plants.AddAsync(validPlant);
            await context.SaveChangesAsync();

            var id = validPlant.Id;
            const string name = "New name :)";
            var cntBefore = await context.Plants.CountAsync();

            var updateCmd = new UpdatePlantCommand
                {Id = validPlant.Id, Name = name, IsActive = true, PlantSpeciesId = plantSpecies1.Id};
            var cmdHandler = new UpdatePlantCommandHandler(context);
            var result = await cmdHandler.Handle(updateCmd, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(cntBefore, await context.Plants.CountAsync());
            var obj = await context.Plants.FindAsync(id);
            Assert.Equal(id, obj.Id);
            Assert.Equal(name, obj.Name);
            Assert.Equal(PlantState.Active, obj.PlantState);
            Assert.Equal(plantSpecies1.Id, obj.PlantSpeciesId);

            var updateCmd2 = new UpdatePlantCommand
                {Id = validPlant.Id, Name = name, IsActive = false, PlantSpeciesId = plantSpecies1.Id};
            var result2 = await cmdHandler.Handle(updateCmd2, CancellationToken.None);
            Assert.True(result2.Success);
            Assert.Equal(cntBefore, await context.Plants.CountAsync());
            obj = await context.Plants.FindAsync(id);
            Assert.Equal(id, obj.Id);
            Assert.Equal(name, obj.Name);
            Assert.Equal(PlantState.Inactive, obj.PlantState);
            Assert.Equal(plantSpecies1.Id, obj.PlantSpeciesId);
        }

        [Fact]
        public async Task QueryAllPlants()
        {
            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(QueryAllPlants));
            var plantSpecies1 = ValidObjectHelper.ValidPlantSpecies();
            var plantSpecies2 = ValidObjectHelper.ValidPlantSpecies();
            await context.PlantSpecies.AddRangeAsync(plantSpecies1, plantSpecies2);

            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();
            var id4 = Guid.NewGuid();
            var id5 = Guid.NewGuid();

            var name1 = "Hello";
            var name2 = "World";
            var name3 = "ab";
            var name4 = "world Hello abcdefgh";
            var name5 = "Töster";

            var plant1 = new Plant
                {Id = id1, Name = name1, PlantSpeciesId = plantSpecies1.Id, PlantState = PlantState.Active};
            var plant2 = new Plant
                {Id = id2, Name = name2, PlantSpeciesId = plantSpecies2.Id, PlantState = PlantState.Active};
            var plant3 = new Plant
                {Id = id3, Name = name3, PlantSpeciesId = plantSpecies1.Id, PlantState = PlantState.Active};
            var plant4 = new Plant
                {Id = id4, Name = name4, PlantSpeciesId = plantSpecies2.Id, PlantState = PlantState.Active};
            var plant5 = new Plant
                {Id = id5, Name = name5, PlantSpeciesId = plantSpecies1.Id, PlantState = PlantState.Active};

            await context.Plants.AddRangeAsync(plant1, plant2, plant3, plant4, plant5);
            await context.SaveChangesAsync();


            var queryAll = new GetAllPlantsQuery();
            var queryHandler = new GetAllPlantsQueryHandler(context);
            var result = await queryHandler.Handle(queryAll, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(5, result.Data.Count());
            Assert.Contains(result.Data,
                dto => dto.Id.Equals(id1) && dto.Name.Equals(name1) && dto.PlantSpeciesId.Equals(plantSpecies1.Id) &&
                       dto.IsActive);
            Assert.Contains(result.Data,
                dto => dto.Id.Equals(id2) && dto.Name.Equals(name2) && dto.PlantSpeciesId.Equals(plantSpecies2.Id) &&
                       dto.IsActive);
            Assert.Contains(result.Data,
                dto => dto.Id.Equals(id3) && dto.Name.Equals(name3) && dto.PlantSpeciesId.Equals(plantSpecies1.Id) &&
                       dto.IsActive);
            Assert.Contains(result.Data,
                dto => dto.Id.Equals(id4) && dto.Name.Equals(name4) && dto.PlantSpeciesId.Equals(plantSpecies2.Id) &&
                       dto.IsActive);
            Assert.Contains(result.Data,
                dto => dto.Id.Equals(id5) && dto.Name.Equals(name5) && dto.PlantSpeciesId.Equals(plantSpecies1.Id) &&
                       dto.IsActive);
        }

        [Fact]
        public async Task GetSinglePlant()
        {
            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(GetSinglePlant));
            var plantSpecies1 = ValidObjectHelper.ValidPlantSpecies();
            await context.PlantSpecies.AddAsync(
                plantSpecies1
            );
            var validPlant = ValidObjectHelper.ValidPlant(plantSpecies1);
            await context.Plants.AddAsync(validPlant);
            await context.SaveChangesAsync();
            
            var id = validPlant.Id;
            var name = validPlant.Name;
            var active = validPlant.PlantState == PlantState.Active;
            var speciesId = validPlant.PlantSpeciesId;
            
            var querySingle = new GetPlantByIdQuery {Id = id};
            var queryHandler = new GetPlantByIdQueryHandler(context);
            var result = await queryHandler.Handle(querySingle, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(id, result.Data.Id);
            Assert.Equal(name, result.Data.Name);
            Assert.Equal(active, result.Data.IsActive);
            Assert.Equal(speciesId, result.Data.PlantSpeciesId);
        
            var queryNonExisting = new GetPlantByIdQuery {Id = Guid.NewGuid()};
            var resultNonExisting = await queryHandler.Handle(queryNonExisting, CancellationToken.None);
            Assert.False(resultNonExisting.Success);
        }
        
        [Fact]
        public async Task DeletePlant()
        {
            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(DeletePlant));
            var plantSpecies1 = ValidObjectHelper.ValidPlantSpecies();
            await context.PlantSpecies.AddAsync(
                plantSpecies1
            );
            var validPlant = ValidObjectHelper.ValidPlant(plantSpecies1);
            await context.Plants.AddAsync(validPlant);
            await context.SaveChangesAsync();

            var id = validPlant.Id;
            var deleteCmd = new DeletePlantCommand {Id = id};
            var deleteHandler = new DeletePlantCommandHandler(context);
            var deleteResult = await deleteHandler.Handle(deleteCmd, CancellationToken.None);
            Assert.True(deleteResult.Success);
            Assert.Empty(context.Plants);
        
            var deleteResult2 = await deleteHandler.Handle(deleteCmd, CancellationToken.None);
            Assert.False(deleteResult2.Success);
        }
    }
}