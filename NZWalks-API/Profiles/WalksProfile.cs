﻿using AutoMapper;

namespace NZWalks_API.Profiles
{
    public class WalksProfile : Profile
    {
        public WalksProfile() 
        {
            CreateMap<Models.Domain.Walk, Models.DTO.Walk>()
                .ReverseMap();

            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>()
                .ReverseMap();
        }
    }
}
