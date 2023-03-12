using AutoMapper;
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
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
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
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
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
    }
}
