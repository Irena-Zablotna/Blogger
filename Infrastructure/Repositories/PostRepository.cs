using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BloggerContext _bloggerContext;
        
        public PostRepository(BloggerContext bloggerContext)
        {
            _bloggerContext = bloggerContext;
        }

        public async Task<IEnumerable<Post>> GetAllAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterBy)
        {

            return await _bloggerContext.Posts
                .Where(p=>p.Title.ToLower().Contains(filterBy.ToLower())|| p.Content.ToLower().Contains(filterBy.ToLower()))
                .OrderByPropertyName(sortField, ascending)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetAllCountAsync(string filterby)
        {
            return await _bloggerContext.Posts
                .Where(p => p.Title.ToLower().Contains(filterby.ToLower()) ||
                p.Content.ToLower().Contains(filterby.ToLower()))
                .CountAsync();
               
        }

        public async Task<Post> GetByIdAsync(int id)
        {
           return await _bloggerContext.Posts.SingleOrDefaultAsync(x=>x.Id==id);
        }


        //public async Task<IEnumerable<Post>> SearchByTitleAsync(string str)
        //{
        //    var selected = _bloggerContext.Posts.Where(x => x.Title
        //    .Contains(str.ToLowerInvariant()));

           
        //    return await selected.ToListAsync();

        //}
        public async Task<Post> AddAsync(Post post)
        {
          
            var createdPost=await _bloggerContext.Posts.AddAsync(post);
            await _bloggerContext.SaveChangesAsync();
            return createdPost.Entity;
        }


        public async Task UpdateAsync(Post post)
        {
            _bloggerContext.Posts.Update(post);
            await _bloggerContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Post post)
        {
            _bloggerContext.Posts.Remove(post);
           await _bloggerContext.SaveChangesAsync();
            await Task.CompletedTask;

        }

       
    }
}