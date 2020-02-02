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
    public class PlantSpeciesController : ApiController
    {
        private readonly IMediator mediator;

        public PlantSpeciesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(ApiRoutes.PlantSpecies.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlantSpecies>> Get(Guid plantSpeciesId)
        {
            var query = new GetPlantSpeciesByIdQuery {Id = plantSpeciesId};
            var response = await mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            return Ok(PlantSpecies.FromCore(response.Data));
        }

        [HttpGet(ApiRoutes.PlantSpecies.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<PlantSpecies>>> GetAll([FromQuery] GetAllPlantSpeciesRequest request)
        {
            var query = new GetAllPlantSpeciesQuery {NameFilter = request.NameFilter};
            var response = await mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            var responseData = response.Data.Select(PlantSpecies.FromCore);
            return Ok(responseData);
        }

        [HttpPost(ApiRoutes.PlantSpecies.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlantSpecies>> Create(CreatePlantSpeciesRequest request)
        {
            var command = new CreatePlantSpeciesCommand {Id = Guid.NewGuid(), Name = request.Name};
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            var createdQuery = new GetPlantSpeciesByIdQuery {Id = command.Id};
            var createdResult = await mediator.Send(createdQuery);
            var createdObj = PlantSpecies.FromCore(createdResult.Data);

            return CreatedAtAction(nameof(Get), new {plantSpeciesId = command.Id}, createdObj);
        }

        [HttpPut(ApiRoutes.PlantSpecies.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlantSpecies>> Update(Guid plantSpeciesId, UpdatePlantSpeciesRequest request)
        {
            var command = new UpdatePlantSpeciesCommand {Id = plantSpeciesId, Name = request.Name};
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            var updatedQuery = new GetPlantSpeciesByIdQuery {Id = command.Id};
            var updatedResult = await mediator.Send(updatedQuery);
            var updatedObj = PlantSpecies.FromCore(updatedResult.Data);

            return Ok(updatedObj);
        }
        
        
        [HttpDelete(ApiRoutes.PlantSpecies.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlantSpecies>> Delete(Guid plantSpeciesId)
        {
            var command = new DeletePlantSpeciesCommand {Id = plantSpeciesId};
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessages);
            }

            return Ok();
        }
    }
}