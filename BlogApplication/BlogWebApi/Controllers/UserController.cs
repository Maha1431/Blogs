using AutoMapper;
using CommonLayer.DataTransferObjects;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace BlogWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> AddUserAsync(AddUserDTO userPost)
        {
            try
            { 
                var userExists = userRepository.FindUser(userPost.EmailId);
                if(userExists != null)
                {
                    return StatusCode(StatusCodes.Status409Conflict, new ErrorDetails { StatusCode = 409, Message = "User already exists!" });
                }
                var result = mapper.Map<Users>(userPost);
                await userRepository.AddUser(result);
                await unitOfWork.SaveChangesAsync();
                return Ok(new { success = true, message = $"User Added successfully" });
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost("login")] 
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            try
            {
                var result = await userRepository.Login(user);      
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"LogIn Successful  for your {user.EmailId},token = {result}" });
                }
                return BadRequest(new { Success = false, message = "Your Password is InCorrect" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ErrorDetails { StatusCode = 401, Message = "Login Failed! Please Enter Valid Credentials" });
            }
        }

        [HttpPost("forgotpassword/{Email}")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            try
            {
                var result=userRepository.ForgotPassword(Email);            
                if(result.IsCompletedSuccessfully)
                {
                    await unitOfWork.SaveChangesAsync();
                    return this.Ok(new { success = true, message = $"OTP is generated" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new ErrorDetails { StatusCode = 401, Message = "Your UserName Is InCorrect" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [Authorize]
        [HttpPut("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO reset)
        {
            try
            {
                var result = await userRepository.ResetPassword(reset);              
                if(true)
                {
                    await unitOfWork.SaveChangesAsync();
                    return this.Ok(new { success = true, message = "Password reset successfully" });
                }            
            }
            catch (Exception)
            {
                // return StatusCode(StatusCodes.Status401Unauthorized, new ErrorDetails { StatusCode = 401, Message = "Your UserName Is InCorrect" });
                return NotFound();
            }
        }

        [HttpPut("updateuser/{Id}")]
        public async Task<IActionResult> UpdateUser(int Id,UpdateUserDTO user)
        {
            try
            {
                var result = mapper.Map<Users>(user);
                var update = userRepository.UpdateUser(Id,result);            
                if (update.IsCompletedSuccessfully)
                {
                    await unitOfWork.SaveChangesAsync();
                    return Ok(new { Success = true, message = "User details updated successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ErrorDetails { StatusCode = 404, Message = "User Not Found" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet("getallusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await userRepository.GetAllUsers();
                if(users == null)
                {
                    return NotFound();
                }
                var result = mapper.Map<List<GetUserDetailsDTO>>(users);
                return Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpDelete("deleteuser/{Id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            try
            {
                var result = await userRepository.DeleteUser(Id);
                await unitOfWork.SaveChangesAsync();
                if (result == true)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpGet("getuserbyId/{Id}")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            try
            {          
                var list = await userRepository.GetUserById(Id);
                if (list == null)
                {
                    return NotFound();
                }
                // var result = mapper.Map<GetUserDetailsDTO>(list); <List<GetUserDetailsDTO>>
                var result = mapper.Map<GetUserDetailsDTO>(list);
                return Ok(result);
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}
