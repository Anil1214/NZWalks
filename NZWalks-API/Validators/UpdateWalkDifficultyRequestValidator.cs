using FluentValidation;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Validators
{
    public class UpdateWalkDifficultyRequestValidator : AbstractValidator<UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyRequestValidator() 
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
