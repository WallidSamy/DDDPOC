using AutoMapper;
using DDDPOC.Application.Commands.Realms;
using DDDPOC.Application.Mapper;
using DDDPOC.Application.ViewModels.Response;
using Keycloak.Net;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Application.Commands.Client
{
    public class GetRealmClientsCommand : IRequest<List<GetClientsOfRealmResponse>>
    {
        public string RealmName { get; set; }
    }

    public class GetRealmClientsCommandHandler : IRequestHandler<GetRealmClientsCommand, List<GetClientsOfRealmResponse>>
    {
        private readonly IConfiguration _config;
        private readonly KeycloakClient _keycloakClient;
        private readonly IMapper _mapper;

        public GetRealmClientsCommandHandler(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
            _keycloakClient = new KeycloakClient(_config.GetSection("KeycloackConfig:Host").Value,
                                                 _config.GetSection("KeycloackConfig:AdminUsername").Value,
                                                 _config.GetSection("KeycloackConfig:AdminPassword").Value,
                                                 new KeycloakOptions(adminClientId: "admin-cli"));
        }
        public async Task<List<GetClientsOfRealmResponse>> Handle(GetRealmClientsCommand request, CancellationToken cancellationToken)
        {
            var clients = await _keycloakClient.GetClientsAsync(request.RealmName);
            return _mapper.Map<List<GetClientsOfRealmResponse>>(clients);
        }
    }
}
