using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedditMonitor.RedditApi
{
    internal class AuthHandler : DelegatingHandler
    {
        private const string userAgentKey = "User-Agent";
        private readonly RedditApiOptions options;
        private readonly ITokenCache tokenCache;
        private readonly bool refreshToken = true;

        public AuthHandler(ITokenCache tokenCache, RedditApiOptions options)
        {
            this.tokenCache = tokenCache;
            this.options = options;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await GetToken(cancellationToken);

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add(userAgentKey, options.UserAgent);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await RefreshAccessToken(cancellationToken);
                return await SendAsync(request, cancellationToken);
            }

            return response;
        }

        private async Task<string> GetToken(CancellationToken cancellationToken)
        {
            var token = tokenCache.GetToken();

            if (string.IsNullOrWhiteSpace(token))
            {
                await RefreshAccessToken(cancellationToken);
            }

            return tokenCache.GetToken();
        }

        private async Task RefreshAccessToken(CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "grant_type", options.GrantType },
            };

            var content = new FormUrlEncodedContent(parameters);
            var authRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(options.TokenUrl))
            {
                Content = content
            };
            string base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{options.ClientId}:{options.ClientSecret}"));
            authRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
            authRequest.Headers.Add(userAgentKey, options.UserAgent);

            var response = await base.SendAsync(authRequest, cancellationToken);

            if (response == null)
            {
                throw new NullReferenceException($"Response to get auth token from reddit was null");
            }

            response.EnsureSuccessStatusCode();

            var authToken = await response.Content.ReadFromJsonAsync<AuthToken>(cancellationToken: cancellationToken);

            if (authToken == null)
            {
                throw new NullReferenceException("Could not deserialize auth token from reddit");
            }

            tokenCache.SetToken(authToken.Access_Token);
        }
    }
}
