using DDDPOC.Application.Commands.Users;
using DDDPOC.Application.ViewModels.Response;
using Keycloak.Net;
using Keycloak.Net.Models.RealmsAdmin;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Application.Commands.Realms
{
    public class GetRealmCommand : IRequest<GetRealmResponse>
    {
        public string RealmName { get; set; }
    }

    /*public class GetRealmCommandHandler : IRequestHandler<GetRealmCommand, GetRealmResponse>
    {
        private readonly IConfiguration _config;
        private readonly KeycloakClient _keycloakClient;

        public GetRealmCommandHandler(IConfiguration config)
        {
            _config = config;
            _keycloakClient = new KeycloakClient(_config.GetSection("KeycloackConfig:Host").Value,
                                                 _config.GetSection("KeycloackConfig:AdminUsername").Value,
                                                 _config.GetSection("KeycloackConfig:AdminPassword").Value,
                                                 new KeycloakOptions(adminClientId: "admin-cli"));
        }
        public async Task<GetRealmResponse> Handle(GetRealmCommand request, CancellationToken cancellationToken)
        {
            var realm = await _keycloakClient.GetClientsAsync(request.RealmName);
        }
    }*/
}
