﻿using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public interface IWalkRepsitory
    {
        Task<IEnumerable<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null);
        Task<Walk> GetAsync(Guid id);
        Task<Walk> AddAsync(Walk walk);
        Task<Walk> UpdateAsync(Guid id, Walk walk);
        Task<Walk> DeleteAsync(Guid id);
    }
}
