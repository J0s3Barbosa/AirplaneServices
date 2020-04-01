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

namespace Services.WebAPI.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [SwaggerGroup("Payment Api")]
    [ApiController, Route("api/v{version:apiVersion}/[controller]"), Produces("application/json")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentLogic _IPaymentLogic;

        public PaymentController(IPaymentLogic iAppPaymentLogic)
        {
            this._IPaymentLogic = iAppPaymentLogic;
        }

        /// <summary>
        /// Get Payment
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IList<Payment>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IList<Payment>>> GetPayment(string Description, string DueDate)
        {
            List<Payment> payments = this._IPaymentLogic.List(Description, DueDate);

            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "X-Total-Count");
            Request.HttpContext.Response.Headers.Add("X-Total-Count", payments?.Count().ToString());

            return await Task.FromResult<ActionResult>(this.Ok(payments));
        }

        /// <summary>
        /// Get Payment
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Payment), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Payment>> GetPayments(string id)
        {
            Guid identifier = Guid.Empty;
            if (!Guid.TryParse(id, out identifier))
                return await Task.FromResult<ActionResult>(this.BadRequest(new ErrorModel(1, "Id", "Invalid ID!").ToList()));

            var payment = this._IPaymentLogic.GetEntity(identifier);

            if (payment == null) return await Task.FromResult<ActionResult>(this.NotFound());
            else return await Task.FromResult<ActionResult>(this.Ok(payment)); ;
        }

        /// <summary>
        /// Create Payment
        /// </summary>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Payment), 201)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Payment>> CreatePayment([FromBody] PaymentAddDTO payment)
        {
            if (payment == null)
                return await Task.FromResult<ActionResult>(this.BadRequest(new ErrorModel(1, "Payment", "The Payment can not be null!").ToList()));
            var dateFormat = "{0:dd/MM/yyyy}";

            var entity = new Payment()
            {
                Description = payment.Description,
                DueDate = string.Format(dateFormat, payment.DueDate),
                BarCode = payment.BarCode,
                PaidDate = string.Format(dateFormat, payment.PaidDate)
            };

            var result = this._IPaymentLogic.AddEntity(entity);

            if (result.Errors.Count > 0) return await Task.FromResult<ActionResult>(this.UnprocessableEntity(result.Errors.First()));
            else return await Task.FromResult<ActionResult>(this.Created(result.Response.Id.ToString(), result.Response));
        }

        /// <summary>
        /// Update Payment
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Payment), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 422)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Payment>> UpdatePayment(string id, [FromBody] PaymentAlterDTO payment)
        {
            Guid identifier = Guid.Empty;
            if (!Guid.TryParse(id, out identifier))
                return await Task.FromResult<ActionResult>(this.BadRequest(new ErrorModel(1, "Id", "Invalid ID!").ToList()));

            if (payment == null)
                return await Task.FromResult<ActionResult>(this.BadRequest(new ErrorModel(1, "Payment", "Payment invalid!").ToList()));
            var dateFormat = "{0:dd/MM/yyyy}";

            var entity = new Payment()
            {
                Description = payment.Description,
                DueDate = string.Format(dateFormat, payment.DueDate),
                BarCode = payment.BarCode,
                PaidDate = string.Format(dateFormat, payment.PaidDate)
            };

            var result = this._IPaymentLogic.Update(identifier, entity);
            if (result.Errors.Count > 0) return await Task.FromResult<ActionResult>(this.UnprocessableEntity(result.Errors.First()));
            else return await Task.FromResult<ActionResult>(this.Ok(result.Response));
        }

        /// <summary>
        /// Remove Payment
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<string>> RemovePayment(string id)
        {
            List<Payment> PaymentRepository = new List<Payment>();//n tava

            Guid identifier = Guid.Empty;
            if (!Guid.TryParse(id, out identifier))
                return await Task.FromResult<ActionResult>(this.BadRequest(new ErrorModel(1, "Id", "Invalid ID!").ToList()));

            var selectedPayment = this._IPaymentLogic.Delete(identifier);
            if (selectedPayment == null) return await Task.FromResult<ActionResult>(this.NotFound());

            if (selectedPayment <= 0) return await Task.FromResult<ActionResult>(this.UnprocessableEntity());
            else return await Task.FromResult<ActionResult>(this.Ok("The Payment was successfully removed."));
        }
    }
}