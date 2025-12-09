using EduTrack.Application.DTOs;

namespace EduTrack.Application.Interfaces;

public interface IUserService
{
    Task<UserDtos.UserResponseDTO?> GetUserByIdAsync(int id);
    Task<UserDtos.UserResponseDTO?> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserDtos.UserResponseDTO>> GetAllUsersAsync();
    Task<UserDtos.UserResponseDTO> CreateUserAsync(UserDtos.UserCreateDTO userCreateDto);
    Task<UserDtos.UserResponseDTO?> UpdateUserAsync(UserDtos.UserUpdateDTO userUpdateDto);
    Task<UserDtos.UserResponseDTO?> DeleteUserAsync(int id);
    
    // 🔥 MODIFICADO: Ahora devuelve AccessToken y RefreshToken
    Task<(string AccessToken, string RefreshToken)> AuthenticateAsync(LoginDto loginDto);
    Task<(string AccessToken, string RefreshToken)> RegisterAsync(RegisterDto registerDto);
    
    // 🔥 NUEVO: Método para refrescar tokens
    Task<(string NewAccessToken, string NewRefreshToken)> RefreshTokenAsync(string accessToken, string refreshToken);
}