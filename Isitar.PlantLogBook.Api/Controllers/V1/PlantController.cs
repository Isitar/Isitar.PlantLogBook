using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Api.Contracts.V1;
using Isitar.PlantLogBook.Api.Contracts.V1.Requests;
using Isitar.PlantLogBook.Api.Contracts.V1.Responses;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Queries;
using Isitar.PlantLogBook.Core.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Isitar.PlantLogBook.Api.Controllers.V1
{
    public class PlantController : ApiController
    {
        private readonly IMediator mediator;

        public PlantController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #region PlantCRUD

        [HttpGet(ApiRoutes.Plant.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Plant>> Get(Guid plantId)
        {
            var query = new GetPlantByIdQuery {Id = plantId};
            var response = await mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            return Ok(Plant.FromCore(response.Data));
        }

        [HttpGet(ApiRoutes.Plant.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<Plant>>> GetAll()
        {
            var query = new GetAllPlantsQuery();
            var response = await mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            var responseData = response.Data.Select(Plant.FromCore);
            return Ok(responseData);
        }

        [HttpPost(ApiRoutes.Plant.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Plant>> Create(CreatePlantRequest request)
        {
            var command = new CreatePlantCommand
                {Id = Guid.NewGuid(), PlantSpeciesId = request.PlantSpeciesId, Name = request.Name};
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            var createdQuery = new GetPlantByIdQuery {Id = command.Id};
            var createdResult = await mediator.Send(createdQuery);
            var createdObj = Plant.FromCore(createdResult.Data);

            return CreatedAtAction(nameof(Get), new {plantId = command.Id}, createdObj);
        }

        [HttpPut(ApiRoutes.Plant.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Plant>> Update(Guid plantId, UpdatePlantRequest request)
        {
            var command = new UpdatePlantCommand
            {
                Id = plantId, PlantSpeciesId = request.PlantSpeciesId, Name = request.Name, IsActive = request.IsActive
            };
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            var updatedQuery = new GetPlantByIdQuery {Id = command.Id};
            var updatedResult = await mediator.Send(updatedQuery);
            var updatedObj = Plant.FromCore(updatedResult.Data);

            return Ok(updatedObj);
        }


        [HttpDelete(ApiRoutes.Plant.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(Guid plantId)
        {
            var command = new DeletePlantCommand {Id = plantId};
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            return Ok();
        }

        #endregion


        [HttpGet(ApiRoutes.Plant.GetPlantLog)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlantLog>> GetPlantLog(Guid plantId, Guid logId)
        {
            var query = new GetPlantLogForPlantByIdQuery {Id = logId, PlantId = plantId};
            var response = await mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            return Ok(PlantLog.FromCore(response.Data));
        }

        [HttpGet(ApiRoutes.Plant.GetAllPlantLogs)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<PlantLog>>> GetAllPlantLogs(Guid plantId, [FromQuery] GetAllPlantLogsForPlantRequest forPlantRequest)
        {
            var query = new GetAllPlantLogsQuery {PlantId = plantId};
            if (null != forPlantRequest.PlantLogTypes)
            {
                query.PlantLogTypes = forPlantRequest.PlantLogTypes;
            }

            if (null != forPlantRequest.FromDateTime)
            {
                query.FromDateTime = forPlantRequest.FromDateTime.Value;
            }

            if (null != forPlantRequest.ToDateTime)
            {
                query.ToDateTime = forPlantRequest.ToDateTime.Value;
            }

            if (!string.IsNullOrWhiteSpace(forPlantRequest.LogFilter))
            {
                query.LogFilter = forPlantRequest.LogFilter;
            }

            var response = await mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            var responseData = response.Data.Select(PlantLog.FromCore);
            return Ok(responseData);
        }


        [HttpPost(ApiRoutes.Plant.CreatePlantLog)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlantLog>> CreatePlantLog(Guid plantId, CreatePlantLogForPlantRequest request)
        {
            var command = new CreatePlantLogForPlantCommand
            {
                Id = Guid.NewGuid(),
                PlantId = plantId,
                PlantLogTypeId = request.PlantLogTypeId,
                Log = request.Log,
                DateTime = request.DateTime,
            };
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            var createdQuery = new GetPlantLogByIdQuery {Id = command.Id};
            var createdResult = await mediator.Send(createdQuery);
            var createdObj = PlantLog.FromCore(createdResult.Data);

            return CreatedAtAction(nameof(GetPlantLog), new {plantId = command.PlantId, logId = command.Id}, createdObj);
        }
    }
}