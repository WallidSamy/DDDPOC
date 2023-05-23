using AutoMapper;
using DDDPOC.Application.ViewModels.Response;
using FS.Keycloak.RestApiClient.Model;
using Keycloak.Net.Models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Application.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserRepresentation, GetAllUsersResponse>();
            CreateMap<Client, GetClientsOfRealmResponse>();
        }
    }
}
