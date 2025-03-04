using RestAPI.Models.DTOs.UserDTO;
using RestAPI.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<AppUser> GetUsers();
        AppUser GetUser(string id);
        bool IsUniqueUser(string userName);
        Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDto);
        Task<UserLoginResponseDTO> Register(UserRegistrationDTO userRegistrationDto);

        // Agrega la firma para obtener los usuarios con su rol:
        Task<List<UserDTO>> GetUserDTOsAsync();
    }
}
