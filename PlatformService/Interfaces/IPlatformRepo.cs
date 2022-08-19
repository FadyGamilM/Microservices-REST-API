using PlatformService.Models;
namespace PlatformService.Interfaces
{
   public interface IPlatformRepo
   {
      // get all platforms 
      Task<IEnumerable<Platform>> GetPlatforms();
      // get platform by id
      Task<Platform> GetPlatformById(int platformID);
      // create new platform
      Task<bool> CreatePlatform(Platform platform);
      // save changes
      bool SaveChanges();
   }
}