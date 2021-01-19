using Domain.ResultTypes;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Interface;
using Infrastructure.ThirdpartyService.IndentityServer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TestDbContext testDbContext) : base(testDbContext)
        {
        }

        public async Task<IS4ApiResponse> GenerateToken(string username, string password)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(IdentityServerConfiguration.Instance.IS4URI) };
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/connect/token");

            List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("scope", "apiscope"),
                new KeyValuePair<string, string>("client_id", IdentityServerConfiguration.Instance.CLIENT_ID),
                new KeyValuePair<string, string>("client_secret", IdentityServerConfiguration.Instance.CLIENT_SECRET),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            };

            request.Content = new FormUrlEncodedContent(keyValues);
            HttpResponseMessage response = await client.SendAsync(request);

            JObject apiResponse = JObject.Parse(await response.Content.ReadAsStringAsync());

            return apiResponse.ToObject<IS4ApiResponse>();
        }

        public async Task<IS4ApiResponse> RefreshToken(string refreshToken)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(IdentityServerConfiguration.Instance.IS4URI) };
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/connect/token");

            List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("scope", "api_poc offline_access"),
                new KeyValuePair<string, string>("client_id", IdentityServerConfiguration.Instance.CLIENT_ID),
                new KeyValuePair<string, string>("client_secret", IdentityServerConfiguration.Instance.CLIENT_SECRET),
                new KeyValuePair<string, string>("refresh_token", refreshToken)
            };

            request.Content = new FormUrlEncodedContent(keyValues);
            HttpResponseMessage response = await client.SendAsync(request);

            JObject apiResponse = JObject.Parse(await response.Content.ReadAsStringAsync());

            return apiResponse.ToObject<IS4ApiResponse>();
        }

        public async Task RevokeToken(string refreshToken)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(IdentityServerConfiguration.Instance.IS4URI) };
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/connect/revocation");

            List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", IdentityServerConfiguration.Instance.CLIENT_ID),
                new KeyValuePair<string, string>("client_secret", IdentityServerConfiguration.Instance.CLIENT_SECRET),
                new KeyValuePair<string, string>("token", refreshToken),
                new KeyValuePair<string, string>("token_type_hint", "refresh_token")
            };

            request.Content = new FormUrlEncodedContent(keyValues);
            await client.SendAsync(request);
        }
    }
}
