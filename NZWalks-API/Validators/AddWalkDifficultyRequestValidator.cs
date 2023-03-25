using FluentValidation;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Validators
{
    public class AddWalkDifficultyRequestValidator : AbstractValidator<AddWalkDifficultyRequest>
    {
        public AddWalkDifficultyRequestValidator() 
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
