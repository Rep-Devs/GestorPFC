using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Models.DTOs.UserDTO;
using RestAPI.Models.DTOs;
using RestAPI.Repository.IRepository;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        protected ResponseApi _responseApi;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _responseApi = new ResponseApi();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                List<UserDTO> userListDto = await _userRepository.GetUserDTOsAsync();
                _responseApi.StatusCode = HttpStatusCode.OK;
                _responseApi.IsSuccess = true;
                _responseApi.Result = userListDto;
                return Ok(_responseApi);
            }
            catch (System.Exception ex)
            {
                _responseApi.StatusCode = HttpStatusCode.InternalServerError;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _responseApi);
            }
        }

        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(string id)
        {
            try
            {
                // Buscar el usuario en la base de datos
                var user = _userRepository.GetUser(id);
                if (user == null)
                {
                    _responseApi.StatusCode = HttpStatusCode.NotFound;
                    _responseApi.IsSuccess = false;
                    _responseApi.ErrorMessages.Add("User not found.");
                    return NotFound(_responseApi);
                }
                
                // Obtener el rol a través de UserManager (o usando el mapeo si ya se configuró en GetUserDTOsAsync)
                // Aquí usamos el mapeo para obtener el DTO, asumiendo que el mapeo se configuró para incluir el rol.
                var userDto = _mapper.Map<UserDTO>(user);
                _responseApi.StatusCode = HttpStatusCode.OK;
                _responseApi.IsSuccess = true;
                _responseApi.Result = userDto;
                return Ok(_responseApi);
            }
            catch (System.Exception ex)
            {
                _responseApi.StatusCode = HttpStatusCode.InternalServerError;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _responseApi);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDTO registrationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { error = "Incorrect Input", message = ModelState });

            if (!_userRepository.IsUniqueUser(registrationDto.UserName))
            {
                _responseApi.StatusCode = HttpStatusCode.BadRequest;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("Username already exists");
                return BadRequest(_responseApi);
            }

            var newUser = await _userRepository.Register(registrationDto);
            if (newUser == null)
            {
                _responseApi.StatusCode = HttpStatusCode.BadRequest;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("Error registering the user");
                return BadRequest(_responseApi);
            }

            _responseApi.StatusCode = HttpStatusCode.OK;
            _responseApi.IsSuccess = true;
            _responseApi.Result = newUser;
            return Ok(_responseApi);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDto)
        {
            var responseLogin = await _userRepository.Login(loginDto);

            if (responseLogin.User == null || string.IsNullOrEmpty(responseLogin.Token))
            {
                _responseApi.StatusCode = HttpStatusCode.BadRequest;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("Incorrect user and password");
                return BadRequest(_responseApi);
            }

            _responseApi.StatusCode = HttpStatusCode.OK;
            _responseApi.IsSuccess = true;
            _responseApi.Result = responseLogin;
            return Ok(_responseApi);
        }
    }
}
