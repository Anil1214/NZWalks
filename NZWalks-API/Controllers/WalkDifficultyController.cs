using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Data;
using NZWalks_API.Models.DTO;
using NZWalks_API.Repositories;
using System.Net.WebSockets;

namespace NZWalks_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalkDifficultyController : ControllerBase
    {
        private readonly IWalkDifficultyRepsitory walkDifficultyRepsitory;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepsitory walkDifficultyRepsitory, IMapper mapper)
        {
            this.walkDifficultyRepsitory = walkDifficultyRepsitory;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultyAsync()
        {
            //Call Repository to get the walk difficulties
            var walkDifficulties = await walkDifficultyRepsitory.GetAllAsync();
            //Convert Domain to DTO
            var walkDifficultiesDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficulties);
            //Return DTO
            return Ok(walkDifficultiesDTO);

        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            //Get WalkDifficulty
            var walkDifficulty = await walkDifficultyRepsitory.GetAsync(id);
            //Cnvert Domain to DTO
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            if(walkDifficultyDTO == null)
            {
                return NotFound();
            }
            //Return DTO to client
            return Ok(walkDifficultyDTO);

        }

        [HttpPost]
        public async Task<IActionResult> CreateWalkDifficultyAsync(AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            //Validate Incoming request
            if (!ValidateCreateWalkDifficultyAsync(addWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }
            //Convert DTO to Domain
            var walkDifficulty = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code,
            };

            //Call repository to save walk Difficulty
            walkDifficulty = await walkDifficultyRepsitory.CreateAsync(walkDifficulty);

            //Convert Domain back to DTO
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            //Return DTO
            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            //Validate Incoming Request
            if (!ValidateUpdateWalkDifficultyAsync(updateWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }
            // Convery DTO to Dmain
            var walkDifficulty = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };

            //Call repository to update the walk difficulty
            walkDifficulty = await walkDifficultyRepsitory.UpdateAsync(id, walkDifficulty);

            if(walkDifficulty  != null)
            {
                return Ok(walkDifficulty);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            //Call Repository to Delete walk Difficulty
            var walkDifficulty = await walkDifficultyRepsitory.DeleteAsync(id);

            //Convert Domain to DTO
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            if( walkDifficultyDTO != null )
            {
                return Ok(walkDifficultyDTO);
            }
            return NotFound();


        }

        #region Private Methods
        private bool ValidateCreateWalkDifficultyAsync(AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if(addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest), $"{nameof(addWalkDifficultyRequest)} Cannot be empty!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest), $"{nameof(addWalkDifficultyRequest.Code)} Cannot be null or empty!");
            }
            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        private bool ValidateUpdateWalkDifficultyAsync(UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (updateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest), $"{nameof(updateWalkDifficultyRequest)} Cannot be empty!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest), $"{nameof(updateWalkDifficultyRequest.Code)} Cannot be null or empty!");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion


    }
}
