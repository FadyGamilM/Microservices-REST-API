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

      [HttpPost("")]
      public async Task<IActionResult> CreatePlatform([FromBody] CreatePlatformDto platformDto)
      {
         if (platformDto == null){
            return BadRequest();
         }
         var platform = this._mapper.Map<Platform>(platformDto);
         var creationResult = await this._platformRepo.CreatePlatform(platform);
         if (creationResult == true){
            return Ok("Created Successfully");
         }else{
            ModelState.AddModelError("", "Error while saving the new platform into the DB");
            return StatusCode(500, ModelState);
         }
      }
      [HttpGet("{ID:int}")]
      public async Task<IActionResult> GetPlatformById([FromRoute] int ID)
      {
         var platform = await this._platformRepo.GetPlatformById(ID);
         if (platform == null){
            return NotFound();
         }else{
            return Ok(this._mapper.Map<ReadPlatformDto>(platform));
         }
      }
   }
}