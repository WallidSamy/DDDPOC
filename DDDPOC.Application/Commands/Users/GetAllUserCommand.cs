using AutoMapper;
using DDDPOC.Application.Mapper;
using DDDPOC.Application.ViewModels.Response;
using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Client;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Application.Commands.Users
{
    public class GetAllUserCommand : IRequest<List<GetAllUsersResponse>>
    {
    }

    public class GetAllUserCommandHandler : IRequestHandler<GetAllUserCommand, List<GetAllUsersResponse>>
    {
        private readonly KeycloakHttpClient _httpClient;
        private readonly UsersApi _usersApi;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public GetAllUserCommandHandler(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
            _httpClient = new KeycloakHttpClient(_config.GetSection("KeycloackConfig:Host").Value, _config.GetSection("KeycloackConfig:AdminUsername").Value, _config.GetSection("KeycloackConfig:AdminPassword").Value);
            _usersApi = ApiClientFactory.Create<UsersApi>(_httpClient);
        }
        public async Task<List<GetAllUsersResponse>> Handle(GetAllUserCommand request, CancellationToken cancellationToken)
        {
            var users = await _usersApi.GetUsersAsync(_config.GetSection("KeycloackConfig:Realm").Value);
            return _mapper.Map<List<GetAllUsersResponse>>(users);
        }
    }
}
