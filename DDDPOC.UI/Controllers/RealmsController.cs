using DDDPOC.Application.Commands.Realms;
using DDDPOC.Application.Commands.Users;
using DDDPOC.Application.ViewModels.Response;
using Keycloak.Net.Models.RealmsAdmin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDDPOC.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RealmsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RealmsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<GetRealmResponse> Get()
        {
            return await _mediator.Send(new GetRealmCommand()); ;
        }
    }
}
