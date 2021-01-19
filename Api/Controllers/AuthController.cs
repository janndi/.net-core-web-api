using Application.Exceptions;
using Application.Requests.Commands;
using Domain.Models.Enums;
using Domain.ResultTypes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("token/connect")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IS4ApiResponse))]
        [Produces("application/json")]
        public async Task<IActionResult> GenerateAuthToken([FromBody] GenerateTokenCommand command)
        {
            var result = await mediator.Send(command);

            if (result.Access_Token == null)
                throw new ApiException(ErrorCodes.BadRequest, result.Error_Description);

            IS4SuccessResponse successResponse = new IS4SuccessResponse
            {
                Access_Token = result.Access_Token,
                Refresh_Token = result.Refresh_Token,
                Expires_In = result.Expires_In,
            };

            return Ok(successResponse);
        }

        #region  Temiporary not available.
        //[HttpPost]
        //[Route("token/refresh")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IS4ApiResponse))]
        //[Produces("application/json")]
        //public async Task<IActionResult> TokenRefresh([FromBody] RefreshTokenCommand command)
        //{
        //    var result = await mediator.Send(command);

        //    if (result.Access_Token == null)
        //        throw new ApiException(ErrorCodes.BadRequest, result.Error_Description);

        //    IS4SuccessResponse successResponse = new IS4SuccessResponse
        //    {
        //        Access_Token = result.Access_Token,
        //        Refresh_Token = result.Refresh_Token,
        //        Expires_In = result.Expires_In
        //    };

        //    return Ok(successResponse);
        //}

        //[HttpPost]
        //[Route("token/revoke")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BooleanResult))]
        //[Produces("application/json")]
        //public async Task<IActionResult> TokenRevoke([FromBody] RevokeTokenCommand command)
        //{
        //    var result = await mediator.Send(command);

        //    return Ok(result);
        //}
        #endregion
    }
}
