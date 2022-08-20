using Microsoft.AspNetCore.Mvc;
using PlatformService.Interfaces;
using PlatformService.DTOs;
using PlatformService.Models;
using AutoMapper;
namespace PlatformService.Controller
{
   [ApiController]
   [Route("api/platforms")]
   public class PlatformController : ControllerBase
   {
      private readonly IMapper _mapper;
      private readonly IPlatformRepo _platformRepo;
      public PlatformController(IMapper mapper, IPlatformRepo platformRepo)
      {
         this._mapper = mapper;
         this._platformRepo = platformRepo;
      }
      [HttpGet("")]
      public async Task<IActionResult> GetPlatforms()
      {
         var platforms = await this._platformRepo.GetPlatforms();
         return Ok(
            this._mapper.Map<IEnumerable<ReadPlatformDto>>(platforms)
         );
      }
      

   }
}