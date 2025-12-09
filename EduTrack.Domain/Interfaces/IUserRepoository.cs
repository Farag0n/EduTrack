using EduTrack.Domain.Entities;

namespace EduTrack.Domain.Interfaces;

public interface IUserRepoository
{
    public Task<User?> GetUserById(int id);
    public Task<User?> GetUserByEmail(string email);
    public Task<IEnumerable<User>> GetAllUsers();
    
    public Task<User> CreateUser(User user);
    public Task<User?> UpdateUser(User user);
    public Task<User?> DeleteUser(int id);
}