using Application.Dto;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public IQueryable<PostDto> GetAllPosts()
        {
           var posts = _postRepository.GetAll();
            return _mapper.ProjectTo<PostDto>(posts);
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterBy)
        {
            var posts = await _postRepository.GetAllAsync(pageNumber, pageSize, sortField, ascending,filterBy);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }


        public async Task<int> GetAllPostsCountAsync(string filterby)
        {
            return await _postRepository.GetAllCountAsync(filterby);
        }  


        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            return _mapper.Map<PostDto>(post);
        }


        //public async Task<IEnumerable<PostDto>> SearchByTitleAsync(string str)
        //{
        //    var selectedPosts = await _postRepository.SearchByTitleAsync(str);
        //    return _mapper.Map<IEnumerable<PostDto>>(selectedPosts);
        //}


        public async Task<PostDto> AddNewPostAsync(CreatePostDto newpostDto, string userId)
        {
            var createdPost = _mapper.Map<Post>(newpostDto);
            createdPost.UserId = userId;
            await _postRepository.AddAsync(createdPost);
            return _mapper.Map<PostDto>(createdPost);
            
        }

        public async Task UpdatePostAsync(UpdatePostDto updatePost)
        {
            var existingPost = await _postRepository.GetByIdAsync(updatePost.Id);
            var post = _mapper.Map(updatePost, existingPost);
            await _postRepository.UpdateAsync(post);
        }

        public async Task DeleteAsync(int id)
        {
            var postToDelete = await _postRepository.GetByIdAsync(id);
            await _postRepository.DeleteAsync(postToDelete);
        }

        public async Task<bool> UserOwnsPostAsync(int postId, string userId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post==null)
            {
                return false;
            }
            if (post.UserId!=userId)
            {
                return false;
            }
            return true;
        }
    }
}
