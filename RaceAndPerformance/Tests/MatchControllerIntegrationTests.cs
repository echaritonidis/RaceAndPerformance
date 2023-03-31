using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RaceAndPerformance.Application.Commands.MatchCommand;
using RaceAndPerformance.Application.Models.Create;
using RaceAndPerformance.Application.Models.Update;
using RaceAndPerformance.Core.Enum;
using Xunit;

namespace RaceAndPerformance.Tests
{
    public class MatchControllerIntegrationTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly ApiWebApplicationFactory _factory;

        public MatchControllerIntegrationTests(ApiWebApplicationFactory factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            // Arrange
            var command = new GetMatchesCommand();
            var json = JsonConvert.SerializeObject(command);
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1.0/match/get-all")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnsOk()
        {
            // Arrange
            var createMatch = new CreateMatch 
            {
                Description = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum",
                MatchDate = "30-03-2023",
                MatchTime = "14:25",
                TeamA = "Team A",
                TeamB = "Team B",
                Sport = SportType.Football,
                MatchOdds = new List<CreateMatchOdd>
                {
                    new CreateMatchOdd()
                    {
                        Specifier = "X",
                        Odd = 1.4
                    }
                }
            };

            var command = new CreateMatchCommand { Match = createMatch };
            var json = JsonConvert.SerializeObject(command);
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1.0/match/create")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_ReturnsOk()
        {
            // Arrange
            var updateMatch = new UpdateMatch
            {
                Id = 1,
                Description = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum",
                MatchDate = "30-03-2023",
                MatchTime = "14:25",
                TeamA = "Team A",
                TeamB = "Team B",
                Sport = SportType.Football,
                MatchOdds = new List<UpdateMatchOdd>
                {
                    new UpdateMatchOdd()
                    {
                        Id = 1,
                        Specifier = "X",
                        Odd = 1.4
                    }
                }
            };

            var command = new UpdateMatchCommand { Match = updateMatch };
            var json = JsonConvert.SerializeObject(command);
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/v1.0/match/update")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var command = new DeleteMatchCommand { Id = 1 };
            var json = JsonConvert.SerializeObject(command);
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/v1.0/match/delete")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}