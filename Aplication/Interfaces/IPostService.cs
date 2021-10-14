using Application.Dto;
using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
   public interface IPostService
    {
        IQueryable<PostDto> GetAllPosts();
        Task<IEnumerable<PostDto>> GetAllPostsAsync(int pageNumber, int pageSize, string sortField, bool Ascending, string filterBy);
        Task<int> GetAllPostsCountAsync(string filterby);
        Task<PostDto> GetPostByIdAsync(int id);
        //Task<IEnumerable<PostDto>> SearchByTitleAsync(string str);
        Task<PostDto> AddNewPostAsync (CreatePostDto newpostDto, string userId);
        Task UpdatePostAsync(UpdatePostDto updatePostDto);
        Task DeleteAsync(int id);
        Task<bool> UserOwnsPostAsync(int postId, string userId);
    }
}
