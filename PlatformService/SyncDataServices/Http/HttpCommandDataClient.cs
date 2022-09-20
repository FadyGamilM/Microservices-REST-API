using System.Text;
using System.Text.Json;
using PlatformService.DTOs;

namespace PlatformService.SyncDataServices.Http
{
   public class HttpCommandDataClient : ICommandDataClient
   {
      private readonly HttpClient _httpClient;
      private readonly IConfiguration _configuration;
      public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
      {
         _httpClient = httpClient;
         _configuration = configuration;
      }

      // send the platform to the command service
      public async Task SendPlatformToCommand(ReadPlatformDto platform)
      {
         var httpContent = new StringContent(
            JsonSerializer.Serialize(platform),
            Encoding.UTF8,
            "application/json"
         );
         
         // make the POST request to the command service
         var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);

         if (response.IsSuccessStatusCode)
         {
            Console.WriteLine("==> Sync POST to command service is done ! ");
         }
         else
         {
            Console.WriteLine("==> [X] Sync POST to command service is not done ! ");
         }
      }
   }
}