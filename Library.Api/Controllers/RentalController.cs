using Library.Application.Commands.RentalCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Api.Controllers
{
    [Route("rental")]
    [ApiController]
    public class RentalController: Controller
    {
        private readonly IMediator _mediator;

        public RentalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateRentalCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
