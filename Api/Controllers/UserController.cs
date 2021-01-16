using Application.Requests.Commands;
using Application.Requests.Queries;
using Domain.Models.DTO;
using Domain.ResultTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    //[Authorize]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BooleanResult))]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await mediator.Send(new DeleteUserCommand(id));

            return Ok(result);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("delete-multiple")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BooleanResult))]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteMultiple(List<Guid> ids)
        {
            var result = await mediator.Send(new DeleteBatchUserCommand(ids));

            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDTO>))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllUserQuery());

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [Produces("application/json")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var result = await mediator.Send(new GetSingleUserQuery(id));

            return Ok(result);
        }

    }
}
