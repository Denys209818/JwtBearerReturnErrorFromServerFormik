using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShowErrorsInFormik.Constants;
using ShowErrorsInFormik.Data.Identity;
using ShowErrorsInFormik.Models;
using ShowErrorsInFormik.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShowErrorsInFormik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMapper _mapper { get; set; }
        private UserManager<AppUser> _userManager { get; set; }
        private IJwtBearerTokenService _tokenService { get; set; }
        private SignInManager<AppUser> _signInManager { get; set; }
        public AccountController(IMapper mapper, UserManager<AppUser> userManager,
            IJwtBearerTokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        [HttpPost]
        [Route("registeruser")]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model) 
        {
            return await Task.Run(() => {
                var user = _mapper.Map<AppUser>(model);
                IActionResult resultOperation = null;

                var result = _userManager.CreateAsync(user, model.Password).Result;

                string fileName = "";
                string fullPath = "";
                if (model.Image != null) 
                {
                    fileName = Path.GetRandomFileName() + Path.GetExtension(model.Image.FileName);
                    fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);

                    using (var stream = System.IO.File.Create(fullPath)) 
                    {
                        model.Image.CopyTo(stream);
                        user.Image = fileName;
                    }
                }

                if (!result.Succeeded) 
                {
                    if (System.IO.File.Exists(fullPath))
                        System.IO.File.Delete(fullPath);


                    resultOperation = BadRequest(new { 
                        errors = result.Errors
                    });

                    return resultOperation;
                }

                var resultRole = _userManager.AddToRoleAsync(user, Roles.USER).Result;
                if (!resultRole.Succeeded) 
                {
                    resultOperation = BadRequest(new
                    {
                        errors = result.Errors
                    });
                    return resultOperation;
                }

                var resSignIn = _signInManager.PasswordSignInAsync(user, model.Password, false, false).Result;
                
                if (!resSignIn.Succeeded) 
                {
                    resultOperation = BadRequest(new
                    {
                        errors = new { 
                            Email="Помилка авторизації"
                        }
                    });

                    return resultOperation;
                }

                resultOperation = Ok(new
                {
                    firstname = user.Firstname,
                    email = user.Email,
                    token = _tokenService.CreateToken(user)
                });

                return resultOperation;
            });
        }
    }
}
