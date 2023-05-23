using DDDPOC.Application.Commands.Client;
using DDDPOC.Application.Commands.Users;
using DDDPOC.Application.ViewModels.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDDPOC.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getRealmClients")]
        public async Task<List<GetClientsOfRealmResponse>> GetRealmClients([FromQuery] GetRealmClientsCommand request)
        {
            return await _mediator.Send(request); ;
        }
    }
}
