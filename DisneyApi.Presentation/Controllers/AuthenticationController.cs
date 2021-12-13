using DisneyApi.Application.Services;
using DisneyApi.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DisneyApi.Presentation.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthenticationController(IUserService service, IConfiguration configuration, IEmailService emailService)
        {
            _service = service;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(UserDtoForCreation user)
        {
            try
            {
                var userEntity = _service.RegisterUser(user);

                if (userEntity != null)
                {
                    _emailService.SendEmailAsync(userEntity.Email);
                    /* Esta funcionalidad puede no estar funcionando al momento de testearla.
                     * Esto se debe a que estoy dejando la secretKey de sendGrid.
                     * SenGrid por cuestiones de seguridad puede bloquear la misma.
                     * El codigo fue testeado y el email es enviado, por lo que si es el caso de que
                     * fue bloqueada con reemplazarla funcionaria sin problemas.
                     */

                    return new JsonResult(user) { StatusCode = 201 };
                }

                return new JsonResult(Conflict()) { StatusCode = 409 };
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UserLogin(UserLoginDto loginUser)
        {
            try
            {
                var usuario = _service.LoginUser(loginUser);

                if (usuario != null)
                {
                    var secretKey = _configuration.GetValue<string>("SecretKey");
                    var key = Encoding.ASCII.GetBytes(secretKey);

                    var claims = new ClaimsIdentity();
                    claims.AddClaim(new Claim("UserId", usuario.UserId.ToString()));
                    claims.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        // Nuestro token va a durar un día
                        Expires = DateTime.UtcNow.AddDays(1),
                        // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                    var token = tokenHandler.WriteToken(createdToken);
                    
                    var response = new LoginResponseDto
                    {
                        Status = "Success",
                        Token = token
                    };

                    return new JsonResult(response) { StatusCode = 201 };
                }

                var errorResponse = new LoginResponseDto
                {
                    Status = "Error",
                    Token = "The token could not be generated"
                };

                return new JsonResult(errorResponse) { StatusCode = 404 };
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
