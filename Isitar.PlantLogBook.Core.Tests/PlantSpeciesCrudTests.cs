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
    public class PlantSpeciesCrudTests
    {
        [Fact]
        public async Task CreatePlantSpecies()
        {
            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(CreatePlantSpecies));

            var validSpecies = ValidObjectHelper.ValidPlantSpecies();
            var id = validSpecies.Id;
            var name = validSpecies.Name;
            var cntBefore = await context.PlantSpecies.CountAsync();

            var createCmd = new CreatePlantSpeciesCommand {Id = id, Name = name};
            var cmdHandler = new CreatePlantSpeciesCommandHandler(context);
            var result = await cmdHandler.Handle(createCmd, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(cntBefore + 1, await context.PlantSpecies.CountAsync());
            var obj = await context.PlantSpecies.FindAsync(id);
            Assert.Equal(id, obj.Id);
            Assert.Equal(name, obj.Name);
        }

        [Fact]
        public async Task UpdatePlantSpecies()
        {
            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(UpdatePlantSpecies));

            var id = Guid.NewGuid();
            await context.PlantSpecies.AddAsync(new PlantSpecies {Id = id, Name = "Something old"});
            await context.SaveChangesAsync();

            var name = "Species1";
            var cntBefore = await context.PlantSpecies.CountAsync();

            var updateCmd = new UpdatePlantSpeciesCommand {Id = id, Name = name};
            var cmdHandler = new UpdatePlantSpeciesCommandHandler(context);
            var result = await cmdHandler.Handle(updateCmd, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(cntBefore, await context.PlantSpecies.CountAsync());
            var obj = await context.PlantSpecies.FindAsync(id);
            Assert.Equal(id, obj.Id);
            Assert.Equal(name, obj.Name);
        }

        [Fact]
        public async Task QueryAllPlantSpecies()
        {
            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(QueryAllPlantSpecies));

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

            var plantSpecies1 = new PlantSpecies {Id = id1, Name = name1};
            var plantSpecies2 = new PlantSpecies {Id = id2, Name = name2};
            var plantSpecies3 = new PlantSpecies {Id = id3, Name = name3};
            var plantSpecies4 = new PlantSpecies {Id = id4, Name = name4};
            var plantSpecies5 = new PlantSpecies {Id = id5, Name = name5};

            await context.PlantSpecies.AddRangeAsync(plantSpecies1, plantSpecies2, plantSpecies3, plantSpecies4,
                plantSpecies5);
            await context.SaveChangesAsync();

            var queryAll = new GetAllPlantSpeciesQuery();
            var queryHandler = new GetAllPlantSpeciesQueryHandler(context);
            var result = await queryHandler.Handle(queryAll, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(5, result.Data.Count());
            Assert.Contains(result.Data, dto => dto.Id.Equals(id1) && dto.Name.Equals(name1));
            Assert.Contains(result.Data, dto => dto.Id.Equals(id2) && dto.Name.Equals(name2));
            Assert.Contains(result.Data, dto => dto.Id.Equals(id3) && dto.Name.Equals(name3));
            Assert.Contains(result.Data, dto => dto.Id.Equals(id4) && dto.Name.Equals(name4));
            Assert.Contains(result.Data, dto => dto.Id.Equals(id5) && dto.Name.Equals(name5));

            var queryAllHello = new GetAllPlantSpeciesQuery {NameFilter = "Hello"};
            var resultAllHello = await queryHandler.Handle(queryAllHello, CancellationToken.None);
            Assert.True(resultAllHello.Success);
            Assert.Equal(2, resultAllHello.Data.Count());
            Assert.Contains(resultAllHello.Data, dto => dto.Id.Equals(id1) && dto.Name.Equals(name1));
            Assert.Contains(resultAllHello.Data, dto => dto.Id.Equals(id4) && dto.Name.Equals(name4));

            var queryAllWorld = new GetAllPlantSpeciesQuery {NameFilter = "World"};
            var resultAllWorld = await queryHandler.Handle(queryAllWorld, CancellationToken.None);
            Assert.True(resultAllWorld.Success);
            Assert.Single(resultAllWorld.Data);
            Assert.Contains(resultAllWorld.Data, dto => dto.Id.Equals(id2) && dto.Name.Equals(name2));

            var queryAllHelloLc = new GetAllPlantSpeciesQuery {NameFilter = "hello"};
            var resultAllHelloLc = await queryHandler.Handle(queryAllHelloLc, CancellationToken.None);
            Assert.True(resultAllHelloLc.Success);
            Assert.Empty(resultAllHelloLc.Data);

            var querySpecialChar = new GetAllPlantSpeciesQuery {NameFilter = "ö"};
            var resultSpecialChar = await queryHandler.Handle(querySpecialChar, CancellationToken.None);
            Assert.True(resultSpecialChar.Success);
            Assert.Single(resultSpecialChar.Data);
            Assert.Contains(resultSpecialChar.Data, dto => dto.Id.Equals(id5) && dto.Name.Equals(name5));
        }

        [Fact]
        public async Task GetSinglePlantSpecies()
        {
            var species = ValidObjectHelper.ValidPlantSpecies();
            var id = species.Id;
            var name = species.Name;

            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(GetSinglePlantSpecies));
            await context.PlantSpecies.AddAsync(new PlantSpecies {Id = id, Name = name});
            await context.SaveChangesAsync();

            var querySingle = new GetPlantSpeciesByIdQuery {Id = id};
            var queryHandler = new GetPlantSpeciesByIdQueryHandler(context);
            var result = await queryHandler.Handle(querySingle, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Id, id);
            Assert.Equal(result.Data.Name, name);

            var queryNonExisting = new GetPlantSpeciesByIdQuery {Id = Guid.NewGuid()};
            var resultNonExisting = await queryHandler.Handle(queryNonExisting, CancellationToken.None);
            Assert.False(resultNonExisting.Success);
        }

        [Fact]
        public async Task DeletePlantSpecies()
        {
            var species = ValidObjectHelper.ValidPlantSpecies();
            var id = species.Id;

            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(DeletePlantSpecies));
            await context.PlantSpecies.AddAsync(species);
            await context.SaveChangesAsync();

            var deleteCmd = new DeletePlantSpeciesCommand {Id = id};
            var deleteHandler = new DeletePlantSpeciesCommandHandler(context);
            var deleteResult = await deleteHandler.Handle(deleteCmd, CancellationToken.None);
            Assert.True(deleteResult.Success);
            Assert.Empty(context.PlantSpecies);

            var deleteResult2 = await deleteHandler.Handle(deleteCmd, CancellationToken.None);
            Assert.False(deleteResult2.Success);
        }
    }
}