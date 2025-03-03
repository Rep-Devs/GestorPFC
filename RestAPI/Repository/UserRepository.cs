using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using RestAPI.Data;
using RestAPI.Models.DTOs.UserDTO;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace RestAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly string _secretKey;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly int TokenExpirationMinutes = 30; // Ajustable según necesidad

        public UserRepository(ApplicationDbContext context, IConfiguration config,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _context = context;
            _secretKey = config.GetValue<string>("ApiSettings:SecretKey");
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public ICollection<AppUser> GetUsers()
        {
            return _context.Users.OrderBy(u => u.UserName).ToList();
        }

        public AppUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        public bool IsUniqueUser(string userName)
        {
            return !_context.Users.Any(u => u.UserName == userName);
        }

        public async Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == userLoginDto.UserName.ToLower());
            if (user == null || !await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
            {
                return new UserLoginResponseDTO { Token = "", User = null };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? "")
                }),
                Expires = System.DateTime.UtcNow.AddMinutes(TokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            return new UserLoginResponseDTO
            {
                Token = tokenHandler.WriteToken(jwtToken),
                User = user
            };
        }

        public async Task<UserLoginResponseDTO> Register(UserRegistrationDTO userRegistrationDto)
        {
            // Determinar el rol a asignar según el email
            string roleToAssign = string.Empty;

            // Buscar asíncronamente en la tabla de Alumnos
            var alumno = await _context.Alumnos
                                       .FirstOrDefaultAsync(a => a.Email.ToLower() == userRegistrationDto.Email.ToLower());
            if (alumno != null)
            {
                roleToAssign = "alumno";
            }
            else
            {
                // Si no es alumno, buscar en la tabla de Profesores
                var profesor = await _context.Profesores
                                             .FirstOrDefaultAsync(p => p.Email.ToLower() == userRegistrationDto.Email.ToLower());
                if (profesor != null)
                {
                    roleToAssign = "profesor";
                }
                else
                {
                    // Si el email no corresponde a ninguno, se rechaza el registro
                    throw new Exception("El email no corresponde a ningún alumno o profesor.");
                }
            }

            // Crear el usuario a partir de los datos del DTO
            AppUser user = new AppUser()
            {
                UserName = userRegistrationDto.UserName,
                Name = userRegistrationDto.Name,
                Email = userRegistrationDto.Email,
                NormalizedEmail = userRegistrationDto.Email.ToUpper()
            };

            // Crear el usuario en Identity
            var result = await _userManager.CreateAsync(user, userRegistrationDto.Password);
            if (!result.Succeeded)
            {
                // Puedes retornar un objeto de error o lanzar una excepción según prefieras
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Error al crear el usuario: {errors}");
            }

            // Asegurarse de que existan los roles básicos
            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (!await _roleManager.RoleExistsAsync("alumno"))
            {
                await _roleManager.CreateAsync(new IdentityRole("alumno"));
            }
            if (!await _roleManager.RoleExistsAsync("profesor"))
            {
                await _roleManager.CreateAsync(new IdentityRole("profesor"));
            }

            // Asignar el rol determinado al usuario
            await _userManager.AddToRoleAsync(user, roleToAssign);

            // Retornar la respuesta (sin token, que se puede generar en el login)
            return new UserLoginResponseDTO
            {
                User = user,
                Token = string.Empty
            };
        }

    }
}
