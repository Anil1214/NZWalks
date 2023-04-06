using FluentValidation;
using NZWalks_API.Models.DTO;
using NZWalks_API.Repositories;

namespace NZWalks_API.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
 
        public LoginRequestValidator() 
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
