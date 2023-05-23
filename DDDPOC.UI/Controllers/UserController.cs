using DDDPOC.Application.Commands.Users;
using DDDPOC.Application.ViewModels.Response;
using DDDPOC.Domain.Aggregates;
using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Client;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DDDPOC.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;
        public  UserController(IMediator mediator) {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<List<GetAllUsersResponse>> GetAll()
        {
            return await _mediator.Send(new GetAllUserCommand());
        }
        [HttpPost("changeStatus")]
        public async Task<bool> ChangeStatus([FromBody] ChangeUserStateCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPost("verifyEmail")]
        public async Task<bool> VerifyEmail([FromBody] VerifyUserEmailCommand command)
        {
            return await _mediator.Send(command);
        }
        [AllowAnonymous]
        [HttpPost("validateInvitation")]
        public async Task<bool> ValidateInvitation([FromBody]  validatemodel model)
        {
            return true;
        }

        public class validatemodel
        {
            public string invitationCode { get; set; }
        }
    }
}
