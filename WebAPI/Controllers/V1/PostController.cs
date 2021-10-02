using Application.Dto;
using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Filters;
using WebAPI.Helpers;
using WebAPI.Helpers.WebAPI.Helpers;
using WebAPI.Wrappers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    //[Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postservice)
        {
            _postService = postservice;
        }

        [SwaggerOperation(Summary = "Retrieve all posts")]
        [HttpGet("[action]")]
        [EnableQuery]
        public IQueryable<PostDto> GetAll()
        {
            return _postService.GetAllPosts();
        }

        [SwaggerOperation(Summary = "Retrieve sort fields")]
        [HttpGet("[action]")]
        //[HttpGet]
        //[Route("GetSortFields")]
        public IActionResult GetSortFields()
        {
            return Ok(SortingHelper.GetSortFields().Select(x => x.Key));
        }

        // GET: api/Posts
        [SwaggerOperation(Summary = "Retrieve paged, filtered and sorted posts")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter filter, [FromQuery] SortingFilter sortingFilter, [FromQuery] string filterBy)
        {
            var validFilter = new PaginationFilter(filter.PageNumber,filter.PageSize);
            var validSortingFilter = new SortingFilter(sortingFilter.SortField, sortingFilter.Ascending);

           var posts = await _postService.GetAllPostsAsync(validFilter.PageNumber, validFilter.PageSize, validSortingFilter.SortField, validSortingFilter.Ascending, filterBy);

            var  totalRecords = await _postService.GetAllPostsCountAsync(filterBy);

            return Ok(PaginationHelper.CreatePagedResponse(posts, validFilter, totalRecords));
        }


        // GET api/Posts/5
        [SwaggerOperation(Summary = "Retrieve a specific post by unique id")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(new Response<PostDto>(post));
        }



        ////Get api/Posts/Search/string

        //[SwaggerOperation(Summary = "Retrieve collection of posts by title")]
        //[HttpGet]
        //[Route("Search/{title}")]
        //public async Task<IActionResult> Search(string title)
        //{
        //    var selected = await _postService.SearchByTitleAsync(title);
        //    return Ok(new Response<IEnumerable<PostDto>>(selected));
        //}


        // POST api/Posts
        [SwaggerOperation(Summary = "Create a new post")]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto newPostDto)
        {
            var post = await  _postService.AddNewPostAsync(newPostDto);
            return Created($"api/posts/{post.Id}", new Response<PostDto>(post));
        }

        // PUT api/Posts/5
        [SwaggerOperation(Summary = "Update a specific post")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update ([FromRoute]int id, [FromBody]UpdatePostDto updatePostDto)
        {
            var result= await _postService.UpdatePostAsync(id, updatePostDto);
           if (result == false)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }

        // DELETE api/Posts/5
        [SwaggerOperation(Summary = "Delete an existing post")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var postToDelete = await _postService.GetPostByIdAsync(id);
            if (postToDelete == null)
            {
                return BadRequest();
            }
            else
            {
               await _postService.DeleteAsync(id);
                return NoContent();
            }
          
        }
    }
}
