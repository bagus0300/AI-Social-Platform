using OpenAI_API.Chat;

namespace AI_Social_Platform.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;

    using FormModels;
    using Newtonsoft.Json;
    using System.Net.Http.Headers;
    using System.Text;
    using OpenAI_API;
    using OpenAI.Images;
    using OpenAI_API.Images;

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
                string textLength = request.TextLength.ToString();
                int length = 100;
                if (textLength == "Short")
                {
                    length = 100;
                }
                else if (textLength == "Middle")
                {
                    length = 250;
                }
                else if (textLength == "Long")
                {
                    length = 500;
                }
                
                var token = configuration["OpenAi:ApiKey"];

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
               
                var requestData = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "user", content = $"Please generate for me a text on {request.Subject} topic, suitable for {request.Audience} audience, in {request.Tone} style and with {length} character length" }
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

        [HttpPost("GenerateAIImage")]
        public async Task<IActionResult> GenerateImage([FromBody] string ImageDescription)
        {
            try
            {
                var apiKey = configuration["OpenAi:ApiKey"]; 
                var api = new OpenAIAPI(apiKey);
                var result = await api.ImageGenerations.CreateImageAsync(ImageDescription, OpenAI_API.Models.Model.DALLE3);

                return Ok(new {imageUrl = result.Data[0].Url});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("GenerateAIImagePicture")]
        public async Task<IActionResult> GenerateImagePicture([FromBody] string ImageDescription)
        {
            try
            {
                var apiKey = configuration["OpenAi:ApiKey"];
                var api = new OpenAIAPI(apiKey);
                var requestSettings = new OpenAI_API.Images.ImageGenerationRequest();
                requestSettings.Prompt = ImageDescription;
                requestSettings.Model = OpenAI_API.Models.Model.DALLE3;
                requestSettings.ResponseFormat = ImageResponseFormat.B64_json;
                requestSettings.Size = OpenAI_API.Images.ImageSize._1024;
                requestSettings.Quality = "standard";
                requestSettings.NumOfImages = 1;

                var result = await api.ImageGenerations.CreateImageAsync(requestSettings);

                var image = result.Data[0].Base64Data;
                var imageBytes = Convert.FromBase64String(image);

                return File(imageBytes, "image/png");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("GenerateAIImageBase64")]
        public async Task<IActionResult> GenerateImageBase64([FromBody] string ImageDescription)
        {
            try
            {
                var apiKey = configuration["OpenAi:ApiKey"];
                var api = new OpenAIAPI(apiKey);
                var requestSettings = new OpenAI_API.Images.ImageGenerationRequest();
                requestSettings.Prompt = ImageDescription;
                requestSettings.Model = OpenAI_API.Models.Model.DALLE3;
                requestSettings.ResponseFormat = ImageResponseFormat.B64_json;
                requestSettings.Size = OpenAI_API.Images.ImageSize._1024;
                requestSettings.Quality = "standard";
                requestSettings.NumOfImages = 1;

                var result = await api.ImageGenerations.CreateImageAsync(requestSettings);

                var image = result.Data[0].Base64Data;

                return Ok(new {imageBase64 = image});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("TranslateAi")]
        public async Task<IActionResult> TranslateAi([FromBody] TranslationRequest request)
        {
            try
            {
                var apiKey = configuration["OpenAi:ApiKey"];
                var openAiApi = new OpenAIAPI(apiKey);
                var response = await openAiApi.Chat.CreateChatCompletionAsync(
                    messages: new[]
                        { 
                            new ChatMessage
                            { Role = ChatMessageRole.System, TextContent =
                                $"You will be provided with a sentence in {request.InputLanguage}, and your task is to translate it into {request.TargetLanguage}."
                            },
                            new ChatMessage
                                { Role = ChatMessageRole.User, TextContent = request.InputToTranslate }
                        },
                    temperature: 0.7,
                    max_tokens: 64
                    );
               

                // Extract the translated text from the API response
                string translatedText = response.Choices[0].Message.TextContent;

                return Ok(new { TranslatedText = translatedText });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }

    public class TranslationRequest
    {
        public string InputLanguage { get; set; } = null!;
        public string InputToTranslate { get; set; } = null!;
        public string TargetLanguage { get; set; } = null!;
    }
}

