using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Application.ViewModels.Response
{
    public class GetClientsOfRealmResponse
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public bool Enabled { get; set; }
        public IEnumerable<string> RedirectUris { get; set; }

        public IEnumerable<object> WebOrigins { get; set; }
        public bool? DirectAccessGrantsEnabled { get; set; }

    }
}
