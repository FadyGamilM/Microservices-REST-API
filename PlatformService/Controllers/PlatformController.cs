using System.Windows.Input;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Interfaces;
using PlatformService.DTOs;
using PlatformService.Models;
using AutoMapper;
using PlatformService.SyncDataServices.Http;
namespace PlatformService.Controller
{
   [ApiController]
   [Route("api/platforms")]
   public class PlatformController : ControllerBase
   {

      private readonly IMapper _mapper;
      private readonly IPlatformRepo _platformRepo;
      private readonly ICommandDataClient _commandDataClient;
      public PlatformController(
         IMapper mapper, 
         IPlatformRepo platformRepo,
         ICommandDataClient commandDataClient
         )
      {
         this._mapper = mapper;
         this._platformRepo = platformRepo;
         this._commandDataClient = commandDataClient;
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
            var createdPlatform = this._mapper.Map<ReadPlatformDto>(platform);
            
            // send the request [sync] to the command service with the new created platform
            try
            {
               await _commandDataClient.SendPlatformToCommand(createdPlatform);
            }
            catch(Exception ex)
            {
               Console.WriteLine("[X] ==> Couldn't send the message to the command service synchrounously : " ,ex.Message);
            }

            // return the response to the client
            return CreatedAtRoute(
               nameof(GetPlatformById),
               new {Id = createdPlatform.Id},
               createdPlatform
            );
         }else{
            ModelState.AddModelError("", "Error while saving the new platform into the DB");
            return StatusCode(500, ModelState);
         }
      }

      [HttpGet("{ID:int}", Name ="GetPlatformById")]
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