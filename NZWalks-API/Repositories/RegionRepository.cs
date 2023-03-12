using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext NZWalksDbContext;
        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.NZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = new Guid();
            await NZWalksDbContext.Regions.AddAsync(region);
            await NZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var region = await NZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null) { return null; }
            NZWalksDbContext.Regions.Remove(region); 
            await NZWalksDbContext.SaveChangesAsync(); 
            return region;
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await NZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null) { return null; };

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Long = region.Long;
            existingRegion.Lat = region.Lat;
            existingRegion.Population = region.Population;

            await NZWalksDbContext.SaveChangesAsync();
            return existingRegion;

        }
        public async Task<Region> GetRegionAsync(Guid id)
        {
            return await NZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Region>> GetRegionsAsync()
        {
            return await NZWalksDbContext.Regions.ToListAsync();
        }
    }
}
