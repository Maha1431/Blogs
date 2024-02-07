using CommonLayer.DataTransferObjects;
using CommonLayer.Security;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {  
        public UserRepository(BlogApplicationContext context) : base(context)
        {
            this.context = context;
        }

        public async Task AddUser(Users entity)
        {
            try
            {
                await Add(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    

        public async Task<string> Login(UserLoginDTO user)
        {
            try
            {
                var result = context.Users.SingleOrDefault(x => x.EmailId == user.EmailId); 
                if(true)
                {              
                    var checkPassword = BCrypt.Net.BCrypt.Verify(user.Password, result.Password);
                    if (checkPassword == true)
                        return await Task.FromResult(GenerateJwtToken.GenerateToken(user.EmailId));
                    else
                        return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateUser(int Id,Users entity)
        {
            try
            {
                var result = await Update(Id,entity);
                if (result == true)
                    return await Task.FromResult(true);
                else
                    throw new Exception();

            }
            catch (Exception e)
            {   
                throw e;
            }
        }
    
        public async Task<bool> DeleteUser(int Id)
        {
            try
            {
                return await Delete(Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Users> GetUserById(int Id)
        {
            try
            {
                return await Get(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ForgotPassword(string Email)
        {
            try
            {
                Users user = context.Users.SingleOrDefault(x => x.EmailId == Email);
                if(user != null)
                {                  
                    user.Otp = RandomNumberGenerator.SecretCode();
                    return await Task.FromResult(true);
                }
                else
                {
                    //return await Task.FromResult(false);
                    return false; 
                }
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ResetPassword(ResetPasswordDTO reset)
        {
            try
            {
                Users user = context.Users.SingleOrDefault(x => x.EmailId == reset.EmailId);
                if (true)
                {
                    if (user.Otp != reset.SecretCode)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        user.Password = BCrypt.Net.BCrypt.HashPassword(reset.Password);
                        return await Task.FromResult(true);
                    }
                }                                            
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> FindUser(string EmailId)
        {
            try
            {
                await Find(x => x.EmailId == EmailId);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            try
            {
                return await GetAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }   
}
