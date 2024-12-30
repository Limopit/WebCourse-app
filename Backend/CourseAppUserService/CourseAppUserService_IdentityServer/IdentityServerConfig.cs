using IdentityServer4.Models;

namespace CourseAppUserService_IdentityServer;

public class IdentityServerConfig
{
    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "client_id",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("client_secret".Sha256())
                },
                AllowedScopes = { "api_scope" },
                AllowOfflineAccess = true,
                RequireConsent = false,
                AccessTokenLifetime = 3600,
                AbsoluteRefreshTokenLifetime = 3600,
                RefreshTokenUsage = TokenUsage.OneTimeOnly
            }
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope("api_scope", "UserService API")
        };
    }
    
}