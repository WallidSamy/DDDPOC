using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Application.KeycloakAuthorization
{
    public static class Policies
    {
        public const string RequireAspNetCoreRole = nameof(RequireAspNetCoreRole);

        public const string RequireRealmRole = nameof(RequireRealmRole);

        public const string RequireClientRole = nameof(RequireClientRole);

        public const string RequireToBeInKeycloakGroupAsReader =
            nameof(RequireToBeInKeycloakGroupAsReader);
    }
}
