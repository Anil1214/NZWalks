using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;

namespace NZWalks_API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>()
                .ReverseMap();
                //.ForMember(des => des.Id, options => options.MapFrom(sr => sr.Id)); // If Property name doesn't match then we should map like this
        }
    }
}
