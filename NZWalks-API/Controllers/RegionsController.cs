using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;
using NZWalks_API.Repositories;

namespace NZWalks_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var Regions =  await regionRepository.GetRegionsAsync();
            //var RegionsDTO = new List<Models.DTO.Region>();
            //Regions.ToList().ForEach(r =>
            //{
            //    var RegionDTO = new Models.DTO.Region()
            //    {
            //        Id = r.Id,
            //        Name = r.Name,
            //        Code = r.Code,
            //        Area = r.Area,
            //        Lat = r.Lat,
            //        Long = r.Long,
            //        Population = r.Population
            //    };
            //    RegionsDTO.Add(RegionDTO);
            //});
            var RegionsDTO = mapper.Map<IEnumerable<Models.DTO.Region>>(Regions);
            return Ok(RegionsDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionByIdAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetRegionByIdAsync(Guid id)
        {
            var region = await regionRepository.GetRegionAsync(id);
            if(region == null)
            {
                return NotFound();  
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Validate Request
            //if (!ValidateAddRegionAsync(addRegionRequest))
            //{
            //    return BadRequest(ModelState);
            //}
            //Convert to Domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,
                Area = addRegionRequest.Area,
            };

            //Saving to Repository
            region = await regionRepository.AddRegionAsync(region);

            //Convert back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id= region.Id,
                Code = region.Code,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
                Area = region.Area,
            };
            return CreatedAtAction(nameof(GetRegionByIdAsync), new {id = region.Id}, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]

        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Delete Region
            var region = await regionRepository.DeleteRegionAsync(id);
            //Return Not Found if Null
            if(region == null)
            {
                return NotFound();
            }
            //Convert Domain to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
                Area = region.Area,

            };

            return Ok(regionDTO);
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]

        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //Validate Request Object
            //if (!ValidateUpdateRegionAsync(updateRegionRequest))
            //{
            //    return BadRequest(ModelState);
            //}
            //Convert DTO to Domain
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population,
                Area = updateRegionRequest.Area,
            };

            // Update region
            region = await regionRepository.UpdateRegionAsync(id, region);


            //If regin is null Return NotFound
            if (region == null)
            {
                return NotFound();
            };

            //Convert back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
                Area = region.Area,
            };

            //return region DTO
            return Ok(regionDTO);



        }

        #region Private members
        private bool ValidateAddRegionAsync(AddRegionRequest addRegionRequest)
        {
            if(addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), $"{addRegionRequest} Cannot be empty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name), $"{addRegionRequest.Name} does not exist");
            }
            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code), $"{addRegionRequest.Code} cannot be empty or null" );
            }
            if(addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area), $"{addRegionRequest.Area} Cannot be less then or equal to zero" );
            }
            if (addRegionRequest.Population <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population), $"{addRegionRequest.Population} Cannot be less then or equal to zero");
            }
            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }


        private bool ValidateUpdateRegionAsync(UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest), $"{updateRegionRequest} Cannot be empty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Name), $"{updateRegionRequest.Name} does not exist");
            }
            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code), $"{updateRegionRequest.Code} cannot be empty or null");
            }
            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area), $"{updateRegionRequest.Area} Cannot be less then or equal to zero");
            }
            if (updateRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Lat), $"{updateRegionRequest.Lat} Cannot be less then or equal to zero");
            }
            if (updateRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Long), $"{updateRegionRequest.Long} Cannot be less then or equal to zero");
            }
            if (updateRegionRequest.Population <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population), $"{updateRegionRequest.Population} Cannot be less then or equal to zero");
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
