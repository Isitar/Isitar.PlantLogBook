using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Core.Data;
using Isitar.PlantLogBook.Core.Data.DAO;
using Isitar.PlantLogBook.Core.Handlers.QueryHandlers;
using Isitar.PlantLogBook.Core.Queries;
using Isitar.PlantLogBook.Core.Responses;
using Isitar.PlantLogBook.Core.Tests.Helpers;
using Xunit;

namespace Isitar.PlantLogBook.Core.Tests
{

    public class PlantLogQueryTestsFixture : IDisposable
    {
        internal PlantLog plantLog1;
        internal PlantLog plantLog2;
        internal PlantLog plantLog3;
        internal PlantLog plantLog4;
        internal PlantLog plantLog5;
        internal GetAllPlantLogsQueryHandler queryHandler;
        internal PlantLogType logType1;
        internal PlantLogType logType2;
        internal PlantSpecies plantSpecies;
        internal Plant plant1;
        internal Plant plant2;
        internal readonly PlantLogBookContext context;

        public PlantLogQueryTestsFixture()
        {
            context = DatabaseHelper.CreateInMemoryDatabaseContext(nameof(PlantLogQueryTests));
            logType1 = ValidObjectHelper.ValidPlantLogType();
            logType2 = ValidObjectHelper.ValidPlantLogType();
            plantSpecies = ValidObjectHelper.ValidPlantSpecies();
            plant1 = ValidObjectHelper.ValidPlant(plantSpecies);
            plant2 = ValidObjectHelper.ValidPlant(plantSpecies);

            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();
            var id4 = Guid.NewGuid();
            var id5 = Guid.NewGuid();

            var log1 = "Hello";
            var log2 = "World";
            var log3 = "ab";
            var log4 = "world Hello abcdefgh";
            var log5 = "Töster";

            var dt1 = new DateTime(2020, 01, 01, 00, 00, 00);
            var dt2 = new DateTime(2020, 01, 01, 18, 00, 00);
            var dt3 = new DateTime(2020, 01, 02, 12, 00, 00);
            var dt4 = new DateTime(2020, 02, 01, 12, 00, 00);
            var dt5 = new DateTime(2019, 01, 01, 12, 00, 00);

            plantLog1 = new PlantLog {Id = id1, DateTime = dt1, Log = log1, Plant = plant1, PlantLogType = logType1};
            plantLog2 = new PlantLog {Id = id2, DateTime = dt2, Log = log2, Plant = plant1, PlantLogType = logType1};
            plantLog3 = new PlantLog {Id = id3, DateTime = dt3, Log = log3, Plant = plant2, PlantLogType = logType2};
            plantLog4 = new PlantLog {Id = id4, DateTime = dt4, Log = log4, Plant = plant2, PlantLogType = logType2};
            plantLog5 = new PlantLog {Id = id5, DateTime = dt5, Log = log5, Plant = plant2, PlantLogType = logType1};

            context.AddRange(logType1, logType2, plantSpecies, plant1, plant2, plantLog1, plantLog2, plantLog3, plantLog4, plantLog5);
            context.SaveChanges();
        }
        
        
        public void Dispose()
        {
            context?.Dispose();
        }           
    }
    
    
    public class PlantLogQueryTests : IClassFixture<PlantLogQueryTestsFixture>
    {
        private readonly PlantLogType logType1;
        private readonly PlantLogType logType2;
        private readonly PlantSpecies plantSpecies;
        private readonly Plant plant1;
        private readonly Plant plant2;
        private readonly PlantLog plantLog1;
        private readonly PlantLog plantLog2;
        private readonly PlantLog plantLog3;
        private readonly PlantLog plantLog4;
        private readonly PlantLog plantLog5;
        private readonly GetAllPlantLogsQueryHandler queryHandler;
        
        public PlantLogQueryTests(PlantLogQueryTestsFixture fixture)
        {
            logType1 = fixture.logType1;
            logType2 = fixture.logType2;
            plantSpecies = fixture.plantSpecies;
            plant1 = fixture.plant1;
            plant2 = fixture.plant2;

            plantLog1 = fixture.plantLog1;
            plantLog2 = fixture.plantLog2;
            plantLog3 = fixture.plantLog3;
            plantLog4 = fixture.plantLog4;
            plantLog5 = fixture.plantLog5;
            queryHandler = new GetAllPlantLogsQueryHandler(fixture.context);
        }
      

        [Fact]
        public async Task TestNoFilter()
        {
            var queryAll = new GetAllPlantLogsQuery();
            var result = await queryHandler.Handle(queryAll, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(5, result.Data.Count());
            Assert.Contains(result.Data, x => x.Id.Equals(plantLog1.Id));
            Assert.Contains(result.Data, x => x.Id.Equals(plantLog2.Id));
            Assert.Contains(result.Data, x => x.Id.Equals(plantLog3.Id));
            Assert.Contains(result.Data, x => x.Id.Equals(plantLog4.Id));
            Assert.Contains(result.Data, x => x.Id.Equals(plantLog5.Id));
        }

        [Fact]
        public async Task TestDateTimeFilter()
        {
            var from2019Query = new GetAllPlantLogsQuery {FromDateTime = new DateTime(2019, 01, 01), ToDateTime = new DateTime(2019, 12, 31, 23, 59, 59, 999)};
            var resultsFrom2019 = await queryHandler.Handle(from2019Query, CancellationToken.None);
            Assert.Single(resultsFrom2019.Data);
            var resultFrom2019Entry = resultsFrom2019.Data.First();
            AssertDaoEqualDto(plantLog5, resultFrom2019Entry);

            var january2020Query = new GetAllPlantLogsQuery {FromDateTime = new DateTime(2020, 01, 01), ToDateTime = new DateTime(2020, 01, 31, 23, 59, 59, 999)};
            var resultJanuary2020 = await queryHandler.Handle(january2020Query, CancellationToken.None);
            Assert.Equal(3, resultJanuary2020.Data.Count());
            Assert.Contains(resultJanuary2020.Data, x => x.Id.Equals(plantLog1.Id));
            Assert.Contains(resultJanuary2020.Data, x => x.Id.Equals(plantLog2.Id));
            Assert.Contains(resultJanuary2020.Data, x => x.Id.Equals(plantLog3.Id));
        }

        [Fact]
        public async Task TestLogTypeFilter()
        {
            var queryLogType1 = new GetAllPlantLogsQuery {PlantLogTypes = new[] {logType1.Id}};
            var resultsLogType1 = await queryHandler.Handle(queryLogType1, CancellationToken.None);
            Assert.Equal(3, resultsLogType1.Data.Count());
            Assert.Contains(resultsLogType1.Data, x => x.Id.Equals(plantLog1.Id));
            Assert.Contains(resultsLogType1.Data, x => x.Id.Equals(plantLog2.Id));
            Assert.Contains(resultsLogType1.Data, x => x.Id.Equals(plantLog5.Id));

            var queryLogType1AndInexistent = new GetAllPlantLogsQuery {PlantLogTypes = new[] {logType1.Id, Guid.NewGuid()}};
            var resultLogType1AndInexistent = await queryHandler.Handle(queryLogType1AndInexistent, CancellationToken.None);
            Assert.Equal(3, resultLogType1AndInexistent.Data.Count());
            Assert.Contains(resultLogType1AndInexistent.Data, x => x.Id.Equals(plantLog1.Id));
            Assert.Contains(resultLogType1AndInexistent.Data, x => x.Id.Equals(plantLog2.Id));
            Assert.Contains(resultLogType1AndInexistent.Data, x => x.Id.Equals(plantLog5.Id));
        }

        [Fact]
        public async Task TestPlantFilter()
        {
            var queryPlant1 = new GetAllPlantLogsQuery {PlantId = plant1.Id};
            var resultPlant1 = await queryHandler.Handle(queryPlant1, CancellationToken.None);
            Assert.Equal(2, resultPlant1.Data.Count());
            Assert.Contains(resultPlant1.Data, x => x.Id.Equals(plantLog1.Id));
            Assert.Contains(resultPlant1.Data, x => x.Id.Equals(plantLog2.Id));
            
            var queryPlant2 = new GetAllPlantLogsQuery {PlantId = plant2.Id};
            var resultPlant2 = await queryHandler.Handle(queryPlant2, CancellationToken.None);
            Assert.Equal(3, resultPlant2.Data.Count());
            Assert.Contains(resultPlant2.Data, x => x.Id.Equals(plantLog3.Id));
            Assert.Contains(resultPlant2.Data, x => x.Id.Equals(plantLog4.Id));
            Assert.Contains(resultPlant2.Data, x => x.Id.Equals(plantLog5.Id));
        }

        [Fact]
        public async Task TestLogFilter()
        {
            var queryAllHello = new GetAllPlantLogsQuery {LogFilter = "Hello"};
            var resultAllHello = await  queryHandler.Handle(queryAllHello, CancellationToken.None);
            Assert.Equal(2, resultAllHello.Data.Count());
            Assert.Contains(resultAllHello.Data, x => x.Id.Equals(plantLog1.Id));
            Assert.Contains(resultAllHello.Data, x => x.Id.Equals(plantLog4.Id));
            
            var queryAllTöster = new GetAllPlantLogsQuery {LogFilter = "Töster"};
            var resultAllTöster = await  queryHandler.Handle(queryAllTöster, CancellationToken.None);
            Assert.Single(resultAllTöster.Data);
            AssertDaoEqualDto(plantLog5, resultAllTöster.Data.First());

        }
        
        private void AssertDaoEqualDto(PlantLog plantLog, PlantLogDto plantLogDto)
        {
            Assert.Equal(plantLog.Id, plantLogDto.Id);
            Assert.Equal(plantLog.Log, plantLogDto.Log);
            Assert.Equal(plantLog.DateTime, plantLogDto.DateTime);
            Assert.Equal(plantLog.PlantLogTypeId, plantLogDto.PlantLogTypeId);
            Assert.Equal(plantLog.PlantId, plantLogDto.PlantId);
        }

    }
}