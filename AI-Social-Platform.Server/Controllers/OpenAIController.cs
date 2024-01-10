namespace AI_Social_Platform.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;

    using FormModels;
    using Newtonsoft.Json;
    using System.Net.Http.Headers;
    using System.Text;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OpenAiController : ControllerBase
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<OpenAiController> logger;
        private readonly IConfiguration configuration;

        public OpenAiController(HttpClient httpClient, ILogger<OpenAiController> logger, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.logger = logger;
            this.configuration = configuration;
        }


        [HttpPost("generateText")]
        public async Task<IActionResult> GenerateText([FromBody] OpenAiRequestFormModel request)
        {
            
            try
            {
                var token = configuration["OpenAi:ApiKey"];

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var requestData = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "user", content = request.Prompt }
                    },
                    temperature = 0.7
                };

                var jsonContent = JsonConvert.SerializeObject(requestData);
                requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                var response = await httpClient.SendAsync(requestMessage);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsAsync<OpenAiResponse>();
                var generatedText = result.Choices[0]?.Message?.Content;

                if (string.IsNullOrWhiteSpace(generatedText))
                {
                    logger.LogError("Invalid response format from OpenAI API");
                    return StatusCode(500, new { message = "An error occurred while processing the response from OpenAI API" });
                }

                return Ok(new { generatedText });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred in OpenAIController");
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        public class OpenAiResponse
        {
            public OpenAiChoice[] Choices { get; set; } = null!;
        }

        public class OpenAiChoice
        {
            public string FinishReason { get; set; } = null!;
            public int Index { get; set; }
            public OpenAiMessage Message { get; set; } = null!;
            public object Logprobs { get; set; }
        }

        public class OpenAiMessage
        {
            public string Role { get; set; } = null!;
            public string Content { get; set; } = null!;
        }

    }
}