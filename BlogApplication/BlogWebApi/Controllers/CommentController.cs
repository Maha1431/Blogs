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
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;
        public CommentController(IUnitOfWork unitOfWork, ICommentRepository commentRepository, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.commentRepository = commentRepository;
            this.mapper = mapper;
        }

        [HttpPost("addcomment")]
        public async Task<ActionResult> AddCommentAsync(AddCommentDTO blogComments)
        {
            try
            {
                var userExists = commentRepository.FindUserAndBlog(blogComments.UserId,blogComments.BlogId);
                if(userExists == Task.FromResult(true))
                {
                    var result = mapper.Map<BlogComments>(blogComments);
                    await commentRepository.AddComment(result);
                    await unitOfWork.SaveChangesAsync();
                    return this.Ok(new { success = true, message = $"Comment is posted successfully" });
                }
                return StatusCode(StatusCodes.Status404NotFound, new ErrorDetails { StatusCode = 404, Message = "User/Blog Not Exists" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpGet("getallcomments")]
        public async Task<IActionResult> GetAllComments()
        {
            try
            {
                var comment = await commentRepository.GetAllComments();
                if (comment == null)
                {
                    return NotFound();
                }
               var result = mapper.Map<List<GetCommentDetailsDTO>>(comment);
                return Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet("getcommentbyId/{Id}")]
        public async Task<IActionResult> GetCommentById(int Id)
        {
            try
            {
                var list = await commentRepository.GetComment(Id);
                if (list == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ErrorDetails { StatusCode = 404, Message = "Comment Not Found" });
                }
                var result = mapper.Map<GetCommentDetailsDTO>(list);
                return this.Ok(new { Success = true, message = $"Comment is displayed successfully", data = result });
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpDelete("deletecomment/{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {   
            try
            {
                var result = await commentRepository.DeleteComment(id);
                await unitOfWork.SaveChangesAsync();
                if(result == true)
                {
                    return this.Ok(new { Success = true, message = $"Comment deleted successfully " });
                }
                return StatusCode(StatusCodes.Status404NotFound, new ErrorDetails { StatusCode = 404, Message = "Comment Not Found" });
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPut("updatecomment/{Id}")]
        public async Task<ActionResult> UpdateComment(int Id, UpdateCommentDTO blogPost)
        {
            try
            {

                var result = mapper.Map<BlogComments>(blogPost);
                var update = commentRepository.UpdateComment(Id, result);
                if (update.IsCompletedSuccessfully)
                {
                    await unitOfWork.SaveChangesAsync();
                    return Ok(new { Success = true, message = "Comment updated successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ErrorDetails { StatusCode = 404, Message = "Comment Not Found" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

