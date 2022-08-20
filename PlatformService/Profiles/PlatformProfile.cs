using PlatformService.DTOs;
using PlatformService.Models;
using AutoMapper;
namespace PlatformService.Profiles
{
   public class PlatformProfile : Profile
   {
      public PlatformProfile()
      {
         CreateMap<Platform, ReadPlatformDto>();
         CreateMap<ReadPlatformDto, Platform>();
         CreateMap<Platform, CreatePlatformDto>();
         CreateMap<CreatePlatformDto, Platform>();
      }
   }
}