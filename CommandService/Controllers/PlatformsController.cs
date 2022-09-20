using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
   [ApiController]
   [Route("api/c/platforms")]
   public class PlatformsController : ControllerBase
   {
      public PlatformsController()
      {
         
      }

      [HttpPost("")]
      public IActionResult TestConnection()
      {
         Console.WriteLine("=> Inbound POST @ Command Service");
         return Ok("[POST] Platforms controller from command service");
      }
   }
}