using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using System.Xml.Linq;

namespace NZWalks_API.Repositories
{
    public class WalkRepository : IWalkRepsitory
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            //return await 
            //    nZWalksDbContext.Walks
            //    .Include(x => x.Region)
            //    .Include(x => x.WalkDifficulty)
            //    .ToListAsync();

            var walks = nZWalksDbContext.Walks.Include(x => x.Region).Include(x => x.WalkDifficulty).AsQueryable();
            // Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false) 
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Length) : walks.OrderByDescending(x => x.Length);
                }
            }

            //Pagenation
            var skipResults = (pageNumber - 1) * pageSize;
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

            return await walks.ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync();
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            //Overriding the Id
            walk.Id = Guid.NewGuid();

            //Add the walko t Repository
            await nZWalksDbContext.Walks.AddAsync(walk);

            await nZWalksDbContext.SaveChangesAsync();
            
            //return response
            return walk;    
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await nZWalksDbContext.Walks.FindAsync(id);
            if(existingWalk != null)
            {
                existingWalk.Name = walk.Name;
                existingWalk.Length = walk.Length;
                existingWalk.RegionId = walk.RegionId;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                await nZWalksDbContext.SaveChangesAsync();
            }
            return existingWalk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await nZWalksDbContext.Walks.FindAsync(id);
            if(existingWalk != null )
            {
                nZWalksDbContext.Walks.Remove(existingWalk);
                await nZWalksDbContext.SaveChangesAsync();
            }
            return existingWalk;
        }
    }
}
