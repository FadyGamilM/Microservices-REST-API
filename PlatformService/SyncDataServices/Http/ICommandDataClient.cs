using PlatformService.DTOs;
namespace PlatformService.SyncDataServices.Http
{
   public interface ICommandDataClient
   {
      // method to send a new created platform to the command service
      // we will send the ReadPlatformDto because we need to send the identifier of this platform with the info of it 
      // so the command service can refernece it
      Task SendPlatformToCommand(ReadPlatformDto platform);

   }
}