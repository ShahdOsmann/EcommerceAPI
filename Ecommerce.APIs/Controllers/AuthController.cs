using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.DAL;
using Ecommerce.BLL;
using Ecommerce.Common;
using FluentValidation;
using FluentValidation.Results;

namespace Ecommerce.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LoginDto> _loginValidator;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            TokenService tokenService,
            IValidator<RegisterDto> registerValidator,
            IValidator<LoginDto> loginValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<GeneralResult>> Register(RegisterDto model)
        {
            ValidationResult validationResult = await _registerValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.ErrorCode)
                    .ToDictionary(
                        g => g.Key ?? "",
                        g => g.Select(e => new Errors { Code = e.ErrorCode ?? "", Message = e.ErrorMessage }).ToList()
                    );

                return BadRequest(GeneralResult.FailResult(errors));
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return BadRequest(GeneralResult.FailResult("Email already registered."));

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(e => new Errors { Code = e.Code, Message = e.Description })
                    .ToList();

                return BadRequest(GeneralResult.FailResult(
                    new Dictionary<string, List<Errors>> { { "IdentityErrors", errors } }
                ));
            }

            return Ok(GeneralResult.SuccessResult("User created successfully."));
        }

        [HttpPost("login")]
        public async Task<ActionResult<GeneralResult<string>>> Login(LoginDto model)
        {
            ValidationResult validationResult = await _loginValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.ErrorCode)
                    .ToDictionary(
                        g => g.Key ?? "",
                        g => g.Select(e => new Errors { Code = e.ErrorCode ?? "", Message = e.ErrorMessage }).ToList()
                    );

                return BadRequest(GeneralResult<string>.FailResult(errors));
            }

             var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(GeneralResult.FailResult("Invalid email or password."));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized(GeneralResult.FailResult("Invalid email or password."));

            var token = _tokenService.CreateToken(user);
            return Ok(GeneralResult<string>.SuccessResult(token, "Login successful."));
        }
    }
}