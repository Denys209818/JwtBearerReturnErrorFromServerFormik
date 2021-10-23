using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ShowErrorsInFormik.Data.Identity;
using ShowErrorsInFormik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowErrorsInFormik.Validators
{
    public class RegisterValidation : AbstractValidator<RegisterViewModel>
    {
        private UserManager<AppUser> _userManager { get; set; }
        public RegisterValidation(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Email).NotEmpty().WithMessage("Поле не може бути пустим!")
                .EmailAddress().WithMessage("Пошта не кореткна!")
                .DependentRules(() => {
                    RuleFor(x => x.Email).Must(UniqueEmail).WithMessage("Користувач зареєстрований!");
                });
            RuleFor(x => x.Password).NotEmpty().WithMessage("Поле не може бути пустим!")
                .MinimumLength(5).WithMessage("Найменша кількість символів - 5");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Поле не може бути пустим!")
                .Equal(y => y.Password);
        }

        public bool UniqueEmail(string email) 
        {
            return _userManager.FindByEmailAsync(email).Result == null;
        }
    }
}
