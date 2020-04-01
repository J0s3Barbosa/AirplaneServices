using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Application.Extensions;
using Services.Application.Interfaces;
using Services.Domain.DTO;
using Services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirplaneServices.WebAPI.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [SwaggerGroup("AirPlaneModel Api")]
    [ApiController, Route("api/v{version:apiVersion}/[controller]"), Produces("application/json")]
    public class AirPlaneModelController : ControllerBase
    {
        private readonly IAirPlaneModelLogic _IAirPlaneModelLogic;

        public AirPlaneModelController(IAirPlaneModelLogic iAppAirPlaneModelLogic)
        {
            _IAirPlaneModelLogic = iAppAirPlaneModelLogic;
        }

        //https://localhost:5000/api/v1/AirPlaneModel
        /// <summary>
        /// Get AirPlaneModel
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IList<AirPlaneModelDTO>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IList<AirPlaneModelDTO>>> GetAirPlaneModel()
        {
            var response = this._IAirPlaneModelLogic.List();

            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "X-Total-Count");
            Request.HttpContext.Response.Headers.Add("X-Total-Count", response?.Count().ToString());

            return await Task.FromResult<ActionResult>(this.Ok(response));
        }

        /// <summary>
        /// Get AirPlaneModel
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AirPlaneModelDTO), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<AirPlaneModelDTO>> GetAirPlaneModels(string id)
        {
            Guid identifier = Guid.Empty;
            if (!Guid.TryParse(id, out identifier))
                return await Task.FromResult<ActionResult>(this.BadRequest(new ErrorModel(1, "Id", "Invalid ID!").ToList()));

            var airPlane = this._IAirPlaneModelLogic.GetEntity(identifier);

            if (airPlane == null) return await Task.FromResult<ActionResult>(this.NotFound());
            else return await Task.FromResult<ActionResult>(this.Ok(airPlane)); ;
        }
    }
}