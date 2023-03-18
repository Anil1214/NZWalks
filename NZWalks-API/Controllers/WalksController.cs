using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Data;
using NZWalks_API.Models.DTO;
using NZWalks_API.Repositories;

namespace NZWalks_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepsitory walkRepsitory;
        private readonly IMapper mapper;

        public WalksController(IWalkRepsitory walkRepsitory, IMapper mapper)
        {
            this.walkRepsitory = walkRepsitory;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            // Get All Walks objects
            var walks = await walkRepsitory.GetAllAsync();

            // Convert Domain to DTO
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);

            // Return DTO
            return Ok(walksDTO);


        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            //Get Walk
            var walk = await walkRepsitory.GetAsync(id);
            //Cnvert to DTO
            var WalkDTO =  mapper.Map<Models.DTO.Walk>(walk);
            //Return Respnse
            return Ok(WalkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest)
        {
            //Convert DTO to Domain
            var walk = new Models.Domain.Walk
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };

            //Adding to Repository
            walk = await walkRepsitory.AddAsync(walk);

            //Converting back to DTO
            var walkDTO = new Models.DTO.Walk
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId,
            };

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            //Convert DTO to Domain Model
            var walk = new Models.Domain.Walk
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
            };
            //Update the record in the database and handle null
            var updatedWak = await walkRepsitory.UpdateAsync(id, walk);

            //Return DTO back to client
            if (updatedWak != null)
            {
                var updatedWakToReturn = new Models.DTO.Walk
                {
                    Id = updatedWak.Id,
                    Name = updatedWak.Name,
                    Length = updatedWak.Length,
                    RegionId = updatedWak.RegionId,
                    WalkDifficultyId = updatedWak.WalkDifficultyId,

                };
                return Ok(updatedWakToReturn);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //Call Repo to Delete the Walk
            var walkDomain = await walkRepsitory.DeleteAsync(id);
            if (walkDomain != null)
            {
                var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);
                return Ok(walkDTO);
            }
            return NotFound(null);
        }
    }
}
