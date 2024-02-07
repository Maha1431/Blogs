using CommonLayer.DataTransferObjects;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository : IRepository<Users>
    {
        Task AddUser(Users entity);
        Task<string> Login(UserLoginDTO user);
        Task<bool> DeleteUser(int Id);
        Task<bool> UpdateUser(int Id,Users entity);
        Task<IEnumerable<Users>> GetAllUsers();
        Task<Users> GetUserById(int Id);
        Task<bool> ForgotPassword(string Email);
        Task<bool> ResetPassword(ResetPasswordDTO reset);
        Task<bool> FindUser(string EmailId);

    } 
}
