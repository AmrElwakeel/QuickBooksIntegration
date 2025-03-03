using Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace Infrastructure.Integrations.QuickBooks
{
    public class QuickBooksAuthService
    {
        private readonly QuickBooksSettings _settings;
        private string _accessToken;

        public QuickBooksAuthService(IOptions<QuickBooksSettings> options)
        {
            _settings = options.Value;
        }

        public string GetAuthorizationUrl()
        {
            var oauth2Client = new OAuth2Client(
                _settings.ClientId,
                "",
                _settings.RedirectUri,
                _settings.Environment
            );

            return oauth2Client.GetAuthorizationURL();
        }

        public async Task<string> ExchangeCodeForToken(string authCode)
        {
            var oauth2Client = new OAuth2Client(
                _settings.ClientId,
                _settings.ClientSecret,
                _settings.RedirectUri
            );

            var tokenResponse = await oauth2Client.GetBearerTokenAsync(authCode);
            _accessToken = tokenResponse.AccessToken; // Store the access token for future API calls
            return _accessToken;
        }
    }
}