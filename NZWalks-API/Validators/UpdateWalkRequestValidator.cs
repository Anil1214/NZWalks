﻿using FluentValidation;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Validators
{
    public class UpdateWalkRequestValidator : AbstractValidator<UpdateWalkRequest>
    {
        public UpdateWalkRequestValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
