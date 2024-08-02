using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlantTemp.Models
{
    public class ChatGPT
    {
        private static readonly string apiKey = "sk-proj-zabeM2pN4JUdC7BVdsIzT3BlbkFJMD6sztGMMEty5rcWWRra"; 
        private static readonly string apiUrl = "https://api.openai.com/v1/chat/completions";

        public async Task<string> GetChatGPTResponse(string message)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "user", content = message }
                    },
                    max_tokens = 300,
                };

                var jsonRequestBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic parsedResponse = JsonConvert.DeserializeObject(jsonResponse);
                    string result = parsedResponse.choices[0].message.content.ToString().Trim();
                    return result;
                }
                else
                {
                    MessageBox.Show(response.StatusCode.ToString());
                    return null;
                }
            }
        }
    }
}
