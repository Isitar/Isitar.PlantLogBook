using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Isitar.PlantLogBook.Api.Contracts.V1;
using Isitar.PlantLogBook.Api.Contracts.V1.Requests;
using Isitar.PlantLogBook.Api.Contracts.V1.Responses;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Isitar.PlantLogBook.Api.Controllers.V1
{
    public class PlantLogTypeController : ApiController
    {
        private readonly IMediator mediator;

        public PlantLogTypeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(ApiRoutes.PlantLogType.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlantLogType>> Get(Guid plantLogTypeId)
        {
            var query = new GetPlantLogTypeByIdQuery {Id = plantLogTypeId};
            var response = await mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            return Ok(PlantLogType.FromCore(response.Data));
        }

        [HttpGet(ApiRoutes.PlantLogType.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<PlantLogType>>> GetAll([FromQuery] GetAllPlantLogTypesRequest request)
        {
            var query = new GetAllPlantLogTypesQuery {NameFilter = request.NameFilter};
            var response = await mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            var responseData = response.Data.Select(PlantLogType.FromCore);
            return Ok(responseData);
        }

        [HttpPost(ApiRoutes.PlantLogType.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlantLogType>> Create(CreatePlantLogTypeRequest request)
        {
            var command = new CreatePlantLogTypeCommand {Id = Guid.NewGuid(), Name = request.Name};
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            var createdQuery = new GetPlantLogTypeByIdQuery {Id = command.Id};
            var createdResult = await mediator.Send(createdQuery);
            var createdObj = PlantLogType.FromCore(createdResult.Data);

            return CreatedAtAction(nameof(Get), new {plantLogTypeId = command.Id}, createdObj);
        }

        [HttpPut(ApiRoutes.PlantLogType.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlantLogType>> Update(Guid plantLogTypeId, UpdatePlantLogTypeRequest request)
        {
            var command = new UpdatePlantLogTypeCommand {Id = plantLogTypeId, Name = request.Name};
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            var updatedQuery = new GetPlantLogTypeByIdQuery {Id = command.Id};
            var updatedResult = await mediator.Send(updatedQuery);
            var updatedObj = PlantLogType.FromCore(updatedResult.Data);

            return Ok(updatedObj);
        }


        [HttpDelete(ApiRoutes.PlantLogType.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(Guid plantLogTypeId)
        {
            var command = new DeletePlantLogTypeCommand {Id = plantLogTypeId};
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            return Ok();
        }
    }
}