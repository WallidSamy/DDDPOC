using AutoMapper;
using DDDPOC.Application.ViewModels.Response;
using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Client;
using Keycloak.Net;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Application.Commands.Users
{
    public class ChangeUserStateCommand : IRequest<bool>
    {
        [Required]
        public string userId { get; set; }
        [Required]
        public bool status { get; set; }
    }

    public class ChangeUserStateCommandHandler : IRequestHandler<ChangeUserStateCommand, bool>
    {
        private readonly IConfiguration _config;
        private readonly KeycloakClient _keycloakClient;

        public ChangeUserStateCommandHandler(IConfiguration config)
        {
            _config = config;
            _keycloakClient = new KeycloakClient(_config.GetSection("KeycloackConfig:Host").Value,
                                                 _config.GetSection("KeycloackConfig:AdminUsername").Value,
                                                 _config.GetSection("KeycloackConfig:AdminPassword").Value,
                                                 new KeycloakOptions(adminClientId: "admin-cli"));
        }
        public async Task<bool> Handle(ChangeUserStateCommand request, CancellationToken cancellationToken)
        {
            var user = await _keycloakClient.GetUserAsync(_config.GetSection("KeycloackConfig:Realm").Value,request.userId);
            if(user is not null && user.Enabled != request.status)
            {
                user.Enabled = request.status;
                return await _keycloakClient.UpdateUserAsync(_config.GetSection("KeycloackConfig:Realm").Value,request.userId, user);
            }
            else
            {
                return false;
            }

        }
    }
}
