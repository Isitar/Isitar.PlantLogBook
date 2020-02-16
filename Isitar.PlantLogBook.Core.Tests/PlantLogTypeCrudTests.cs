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
    public class PlantLogTypeCrudTests
    {
        [Fact]
        public async Task CreatePlantLogType()
        {
            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(CreatePlantLogType));

            var validLogType = ValidObjectHelper.ValidPlantLogType();
            var id = validLogType.Id;
            var name = validLogType.Name;
            var cntBefore = await context.PlantLogTypes.CountAsync();

            var createCmd = new CreatePlantLogTypeCommand {Id = id, Name = name};
            var cmdHandler = new CreatePlantLogTypeCommandHandler(context);
            var result = await cmdHandler.Handle(createCmd, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(cntBefore + 1, await context.PlantLogTypes.CountAsync());
            var obj = await context.PlantLogTypes.FindAsync(id);
            Assert.Equal(id, obj.Id);
            Assert.Equal(name, obj.Name);
        }

        [Fact]
        public async Task UpdatePlantLogType()
        {
            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(UpdatePlantLogType));

            var id = Guid.NewGuid();
            await context.PlantLogTypes.AddAsync(new PlantLogType {Id = id, Name = "Something old"});
            await context.SaveChangesAsync();

            var name = "LogType1";
            var cntBefore = await context.PlantLogTypes.CountAsync();

            var updateCmd = new UpdatePlantLogTypeCommand {Id = id, Name = name};
            var cmdHandler = new UpdatePlantLogTypeCommandHandler(context);
            var result = await cmdHandler.Handle(updateCmd, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(cntBefore, await context.PlantLogTypes.CountAsync());
            var obj = await context.PlantLogTypes.FindAsync(id);
            Assert.Equal(id, obj.Id);
            Assert.Equal(name, obj.Name);
        }

        [Fact]
        public async Task QueryAllPlantLogType()
        {
            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(QueryAllPlantLogType));

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

            var plantLogType1 = new PlantLogType {Id = id1, Name = name1};
            var plantLogType2 = new PlantLogType {Id = id2, Name = name2};
            var plantLogType3 = new PlantLogType {Id = id3, Name = name3};
            var plantLogType4 = new PlantLogType {Id = id4, Name = name4};
            var plantLogType5 = new PlantLogType {Id = id5, Name = name5};

            await context.PlantLogTypes.AddRangeAsync(plantLogType1, plantLogType2, plantLogType3, plantLogType4,
                plantLogType5);
            await context.SaveChangesAsync();

            var queryAll = new GetAllPlantLogTypesQuery();
            var queryHandler = new GetAllPlantLogTypesQueryHandler(context);
            var result = await queryHandler.Handle(queryAll, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(5, result.Data.Count());
            Assert.Contains(result.Data, dto => dto.Id.Equals(id1) && dto.Name.Equals(name1));
            Assert.Contains(result.Data, dto => dto.Id.Equals(id2) && dto.Name.Equals(name2));
            Assert.Contains(result.Data, dto => dto.Id.Equals(id3) && dto.Name.Equals(name3));
            Assert.Contains(result.Data, dto => dto.Id.Equals(id4) && dto.Name.Equals(name4));
            Assert.Contains(result.Data, dto => dto.Id.Equals(id5) && dto.Name.Equals(name5));

            var queryAllHello = new GetAllPlantLogTypesQuery {NameFilter = "Hello"};
            var resultAllHello = await queryHandler.Handle(queryAllHello, CancellationToken.None);
            Assert.True(resultAllHello.Success);
            Assert.Equal(2, resultAllHello.Data.Count());
            Assert.Contains(resultAllHello.Data, dto => dto.Id.Equals(id1) && dto.Name.Equals(name1));
            Assert.Contains(resultAllHello.Data, dto => dto.Id.Equals(id4) && dto.Name.Equals(name4));

            var queryAllWorld = new GetAllPlantLogTypesQuery {NameFilter = "World"};
            var resultAllWorld = await queryHandler.Handle(queryAllWorld, CancellationToken.None);
            Assert.True(resultAllWorld.Success);
            Assert.Single(resultAllWorld.Data);
            Assert.Contains(resultAllWorld.Data, dto => dto.Id.Equals(id2) && dto.Name.Equals(name2));

            var queryAllHelloLc = new GetAllPlantLogTypesQuery {NameFilter = "hello"};
            var resultAllHelloLc = await queryHandler.Handle(queryAllHelloLc, CancellationToken.None);
            Assert.True(resultAllHelloLc.Success);
            Assert.Empty(resultAllHelloLc.Data);

            var querySpecialChar = new GetAllPlantLogTypesQuery {NameFilter = "ö"};
            var resultSpecialChar = await queryHandler.Handle(querySpecialChar, CancellationToken.None);
            Assert.True(resultSpecialChar.Success);
            Assert.Single(resultSpecialChar.Data);
            Assert.Contains(resultSpecialChar.Data, dto => dto.Id.Equals(id5) && dto.Name.Equals(name5));
        }

        [Fact]
        public async Task GetSinglePlantLogType()
        {
            var logType = ValidObjectHelper.ValidPlantLogType();
            var id = logType.Id;
            var name = logType.Name;

            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(GetSinglePlantLogType));
            await context.PlantLogTypes.AddAsync(new PlantLogType {Id = id, Name = name});
            await context.SaveChangesAsync();

            var querySingle = new GetPlantLogTypeByIdQuery {Id = id};
            var queryHandler = new GetPlantLogTypeByIdQueryHandler(context);
            var result = await queryHandler.Handle(querySingle, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(id, result.Data.Id);
            Assert.Equal(name, result.Data.Name);

            var queryNonExisting = new GetPlantLogTypeByIdQuery {Id = Guid.NewGuid()};
            var resultNonExisting = await queryHandler.Handle(queryNonExisting, CancellationToken.None);
            Assert.False(resultNonExisting.Success);
        }

        [Fact]
        public async Task DeletePlantLogType()
        {
            var logType = ValidObjectHelper.ValidPlantLogType();
            var id = logType.Id;

            await using var context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(DeletePlantLogType));
            await context.PlantLogTypes.AddAsync(logType);
            await context.SaveChangesAsync();

            var deleteCmd = new DeletePlantLogTypeCommand {Id = id};
            var deleteHandler = new DeletePlantLogTypeCommandHandler(context);
            var deleteResult = await deleteHandler.Handle(deleteCmd, CancellationToken.None);
            Assert.True(deleteResult.Success);
            Assert.Empty(context.PlantLogTypes);

            var deleteResult2 = await deleteHandler.Handle(deleteCmd, CancellationToken.None);
            Assert.False(deleteResult2.Success);
        }
    }
}