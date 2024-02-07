using AutoMapper;
using CommonLayer.DataTransferObjects;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBlogRepository blogRepository;
        private readonly IMapper mapper;
        public BlogController(IUnitOfWork unitOfWork, IBlogRepository blogRepository, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.blogRepository = blogRepository;
            this.mapper = mapper;
        }

        [HttpPost("addblog")]
        public async Task<ActionResult> AddBlogAsync(AddBlogDTO blogPost)
        {
            try
            {
                var userExists=blogRepository.FindUser(blogPost.UserId);
                if(userExists == Task.FromResult(true))
                { 
                    var blogExists = blogRepository.FindBlogTitle(blogPost.Title);
                    if (blogExists != null)
                    {
                        return StatusCode(StatusCodes.Status409Conflict, new ErrorDetails { StatusCode = 409, Message = "Blog already exists!" });
                    }

                    var result = mapper.Map<Blogs>(blogPost);
                    await blogRepository.AddBlog(result);
                    await unitOfWork.SaveChangesAsync();
                    return this.Ok(new { success = true, message = $"Blog is posted successfully" });
                }
                return StatusCode(StatusCodes.Status404NotFound, new ErrorDetails { StatusCode = 404, Message = "User Not Found" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut("updateblog/{Id}")]
        public async Task<IActionResult> UpdateBlog(int Id, UpdateBlogDTO blog)
        {
            try
            {

                var result = mapper.Map<Blogs>(blog);
                var update = blogRepository.UpdateBlog(Id, result);
                if (update.IsCompletedSuccessfully)
                {
                    await unitOfWork.SaveChangesAsync();
                    return Ok(new { Success = true, message = "Blog details updated successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ErrorDetails { StatusCode = 404, Message = "Blog Not Found" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpDelete("deleteblog/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            try
            {
                var result = await blogRepository.DeleteBlog(id);
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

        [HttpGet("getallblogs")]
        public async Task<IActionResult> GetAllBlogs()
        {
            try
            {
                var blogs = await blogRepository.GetAllBlogs();
                if (blogs == null)
                {
                    return NotFound();
                }
                var result = mapper.Map<List<GetBlogDetailsDTO>>(blogs);
                return Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("getblogbyId/{Id}")]
        public async Task<IActionResult> GetBlogByIdAsync(int Id)
        {
            try
            {
                var list = await blogRepository.GetBlog(Id);
                if (list == null)
                {
                    return NotFound();
                }
                var result = mapper.Map<GetBlogDetailsDTO>(list);
                return Ok();
            }   
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
