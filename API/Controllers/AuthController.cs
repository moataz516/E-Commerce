using E_Website.Models;
using E_Website.Models.Data;
using E_Website.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Website.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<applicationUser> _userManager;
        public AuthController(ModelContext context, IConfiguration config,
            UserManager<applicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        //    [HttpPost("login")]
        //    [AllowAnonymous]
        //    public IActionResult Login([FromBody] User userLogin )
        //    {
        //        var user = Authenticate(userLogin);

        //        if(user != null)
        //        {
        //            var token = Generate(user);

        //           return Ok(token);
        //        }
        //        return NotFound("User not found");
        //    }

        //    [Authorize(Roles ="Usera")]
        //    [AllowAnonymous]
        //    [HttpPost("register")]
        //    public IActionResult Register([FromBody] register userRegister) 
        //    {
        //        var userReg = _context.Users.FirstOrDefault(x=>x.email == userRegister.email);
        //        if(userReg != null)
        //        {
        //            return NotFound("User  found");
        //        }
        //        user user = new user()
        //        {
        //            name = userRegister.name,
        //            email = userRegister.email,
        //            password = userRegister.password,
        //            roleName = "User"

        //        };

        //        _context.Users.Add(user);
        //        _context.SaveChanges();
        //        var token = Generate(user);
        //        return Ok(token);
        //    }
        //    private string Generate(user u)
        //    {
        //        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        //        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //        var claims = new[]
        //        {
        //            new Claim(ClaimTypes.NameIdentifier, u.name),
        //            new Claim(ClaimTypes.Email, u.email),
        //            new Claim(ClaimTypes.Role, u.roleName)
        //        };
        //        var token = new JwtSecurityToken(_config["JWT:Issuer"], _config["JWT:Audience"],
        //            claims,expires:DateTime.Now.AddMinutes(15),signingCredentials:credentials);

        //        return new JwtSecurityTokenHandler().WriteToken(token);
        //    }

        //    private user Authenticate(User userLogin)
        //    {
        //        var currentUser = _context.Users.FirstOrDefault(x=>x.email == userLogin.Username && x.password == userLogin.Password);
        //        if(currentUser != null) 
        //        { 
        //            return currentUser; 
        //        }
        //        return null;    
        //    }


        //    [HttpGet("get")]
        //    public IActionResult getUser()
        //    {
        //        var user = _context.Users.ToList();
        //        return Ok(user);
        //    }


        [HttpPost("signup")]
        public async Task<AuthVM> Register([FromBody] RegisterVM model) {


            var checkUserEmail = await _userManager.FindByEmailAsync(model.email);
            if(checkUserEmail != null) {
                return new AuthVM { Message = "Email is already registered" };
            }
            var checkByUsername = await _userManager.FindByNameAsync(model.userName);
            if(checkByUsername != null)
            {
                return new AuthVM { Message = "Username is already registered" };
            }
            var user = new applicationUser { 
                firstName= model.firstName,
                lastName= model.lastName,
                UserName = model.userName,
                Email = model.email,
                PhoneNumber = model.phone
            };

            var result = await _userManager.CreateAsync(user,model.password);
            if(!result.Succeeded) {
                var errors = string.Empty;
                foreach(var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthVM { Message = errors };
            }
            await _userManager.AddToRoleAsync(user,"User");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthVM
            {
                Email = user.Email,
                Expiration = DateTime.Now.AddMinutes(100),
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName
            };
        }

        [HttpPost("signin")]
        public async Task<AuthVM> Login([FromBody]LoginVM model)
        {
            var authVM = new AuthVM();
            var user = await _userManager.FindByEmailAsync(model.Username);
            
            if(user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    authVM.Message = "Email or Password is incorrect!";
                    return authVM;
                }
            }
            var jwtSecurityToken = await CreateJwtToken(user);
            var roleList = await _userManager.GetRolesAsync(user);

            authVM.IsAuthenticated = true;
            authVM.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authVM.Email = user.Email;
            authVM.UserName = user.UserName;
            authVM.Expiration = jwtSecurityToken.ValidTo;
            authVM.Roles = roleList.ToList();

            return authVM;
        }




        private async Task<JwtSecurityToken> CreateJwtToken(applicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach(var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Uid",user.Id)
            }.Union(userClaims).Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var signinCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(_config["JWT:Issuer"], _config["JWT:Audience"],
                claims, expires: DateTime.Now.AddDays(1), signingCredentials: signinCredentials);

            return jwtSecurityToken;

        }

        //[Authorize(Roles ="Adminn")]
        //[HttpPost("add-role")]
        //public async Task<IActionResult> AddRoleAsync([FromBody]AddRoleVM model)
        //{
        //    var role = new IdentityRole { Name = model.Role };
        //    await _roleManager.CreateAsync(role);

            
        //    return Ok("succ");

        //}
    }
}
