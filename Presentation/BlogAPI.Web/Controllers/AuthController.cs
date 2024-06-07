using BlogAPI.Application.Features.Users.Commands.LoginUser;
using BlogAPI.Application.Features.Users.Commands.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse loginUserCommandResponse = await _mediator.Send(loginUserCommandRequest);
            return Ok(loginUserCommandResponse);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromForm]RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
           RefreshTokenLoginCommandResponse loginUserCommandResponse = await _mediator.Send(refreshTokenLoginCommandRequest);
            return Ok(loginUserCommandResponse);
        }



    }
}
