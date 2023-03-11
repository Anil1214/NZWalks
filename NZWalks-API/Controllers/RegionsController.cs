using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Models.Domain;
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
        public async Task<IActionResult> GetAllRegions()
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
    }
}
