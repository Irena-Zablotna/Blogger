using Application.Dto;
using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Filters;
using WebAPI.Helpers;
using WebAPI.Wrappers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace WebAPI.Controllers.V2
//{   
//    [ApiExplorerSettings(IgnoreApi = true)]
//    [ApiVersion("2.0")]
//    [Route("api/{v:apiVersion}/[controller]")]
//    [ApiController]
//    public class PostsController : ControllerBase
//    {
//        private readonly IPostService _postService;

//        public PostsController(IPostService postservice)
//        {
//            _postService = postservice;
//        }



//        // GET: api/<PostController>
//        public async Task<IActionResult> Get([FromQuery] PaginationFilter filter, [FromQuery] SortingFilter sortingFilter)
//        {
//            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
//            var validSortingFilter = new SortingFilter(sortingFilter.SortField, sortingFilter.Ascending);

//            var posts = await _postService.GetAllPostsAsync(validFilter.PageNumber, validFilter.PageSize, validSortingFilter.SortField, validSortingFilter.Ascending);

//            var totalRecords = await _postService.GetAllPostsCountAsync();

//            return Ok(PaginationHelper.CreatePagedResponse(posts, validFilter, totalRecords));
//        }

//        // GET api/<PostController>/5
//        [SwaggerOperation(Summary = "Retrieve a specific post by unique id")]
//        [HttpGet("{id}")]
//        public async Task<IActionResult> Get(int id)
//        {
//            var post = await _postService.GetPostByIdAsync(id);
//            if (post == null)
//            {
//                return NotFound();
//            }

//            return Ok(post);
//        }

//        // POST api/<ValuesController>
//        [SwaggerOperation(Summary = "Create a new post")]
//        [HttpPost]
//        public async Task<IActionResult> Create(CreatePostDto newPostDto)
//        {
//            var post = await _postService.AddNewPostAsync(newPostDto);
//            return Created($"api/posts/{post.Id}", post);
//        }

//        // PUT api/<ValuesController>/5
//        [SwaggerOperation(Summary = "Update a specific post")]
//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePostDto updatePostDto)
//        {
//            var result = await _postService.UpdatePostAsync(id, updatePostDto);
//            if (result == false)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return NoContent();
//            }
//        }

//        // DELETE api/<ValuesController>/5
//        [SwaggerOperation(Summary = "Delete an existing post")]
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var postToDelete = await _postService.GetPostByIdAsync(id);
//            if (postToDelete == null)
//            {
//                return BadRequest();
//            }
//            else
//            {
//                await _postService.DeleteAsync(id);
//                return NoContent();
//            }

//        }
//    }
//}
