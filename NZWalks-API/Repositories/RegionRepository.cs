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


        public async Task<IEnumerable<Region>> GetRegionsAsync()
        {
            return await NZWalksDbContext.Regions.ToListAsync();
        }
    }
}
