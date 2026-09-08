using WorkoutControl.Api.Prompts;
using System.Threading;

namespace WorkoutControl.Api.Services
{
    public class LlamaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _llamaApiUrl;



        public LlamaService(IConfiguration config)
        {
            _httpClient = new HttpClient();
            _llamaApiUrl = config["Llama:ApiUrl"] ?? "http://localhost:11434/api/generate";
        }

        public async Task<string> SendPromptAsync(string prompt)
        {
            var request = new
            {
                prompt = LlamaPrompts.workOutBasePrompt + prompt,
                model = "llama3.1:8b"
            };
            
            _httpClient.Timeout = Timeout.InfiniteTimeSpan; // No timeout
            //TimeSpan.FromSeconds(300); // for example, 5 minutes
            var response = await _httpClient.PostAsJsonAsync(_llamaApiUrl, request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<LlamaResponse>();
            return result?.response ?? "";
        }

        private class LlamaResponse
        {
            public string response { get; set; }
        }
    }
}
