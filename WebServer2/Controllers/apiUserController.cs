using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebServer2.Data;
using WebServer2.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;



namespace WebServer2.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class apiUsersController : Controller
    {
        private readonly WebServer2Context _context;
        public IConfiguration _configuration;


        public apiUsersController(WebServer2Context context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string a = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            Contact contacts = await _context.Contact.FirstOrDefaultAsync(m => m.Id == a);
            return Json(a);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            Signin(user);
            return Json(user);
        }

        public async void Signin(User c)
        {
            HttpContext.Session.SetString("username", c.Id);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, c.Id),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {

            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        [HttpPost]
        public IActionResult post(string id, string password)
        {
            if (true) //auth
            {
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWTParams:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", id)
                    };
                
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:SecretKey"]));
                var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["JWTParams:Issuer"],
                    _configuration["JWTParams:Issuer"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(20),
                    signingCredentials: mac);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return BadRequest();
        }


        private bool UserExists(string id)
        {
          return _context.User.Any(e => e.Id == id);
        }
    }
}
