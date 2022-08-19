using System;
using PlatformService.Interfaces;
using PlatformService.Models;
using PlatformService.Data;
using Microsoft.EntityFrameworkCore;

namespace PlatformService.Repositories
{
   public class PlatformRepo : IPlatformRepo
   {
      // DI pattern
      private readonly AppDbContext _context;
      public PlatformRepo(AppDbContext context)
      {
         this._context = context;
      }
      public async Task<bool> CreatePlatform(Platform platform)
      {
         await this._context.Platforms.AddAsync(platform);
         var creationResult = this.SaveChanges();
         return creationResult;
      }

      public async Task<Platform> GetPlatformById(int platformID)
      {
         var platform = await this._context.Platforms
                              .Where(p => p.Id == platformID)
                              .FirstOrDefaultAsync();
         return platform;
      }

      public async Task<IEnumerable<Platform>> GetPlatforms()
      {
         var platforms = await this._context.Platforms.ToListAsync();
         return platforms;
      }

      public bool SaveChanges()
      {
         return (bool)(this._context.SaveChanges() > 0);
      }
   }
}