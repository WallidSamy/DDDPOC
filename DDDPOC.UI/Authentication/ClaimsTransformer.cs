using DDDPOC.UI.Keycloack;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace DDDPOC.UI.Authentication
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        private readonly IConfiguration _config;
        public ClaimsTransformer(IConfiguration config)
        {
            _config = config;
        }
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)principal.Identity;

            if (claimsIdentity.IsAuthenticated && claimsIdentity.HasClaim((claim) => claim.Type == "resource_access"))
            {
                var userRole = claimsIdentity.FindFirst((claim) => claim.Type == "resource_access");

                var content = Newtonsoft.Json.Linq.JObject.Parse(userRole.Value);
                var client_id = _config.GetValue<string>("KeycloackConfig:Client_Id");
                var roles = content[client_id]?["roles"];
                if(roles is not null)
                {
                    foreach (var role in roles)
                    {
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
                    }
                }
                
            }

            return Task.FromResult(principal);
        }
    }
}
