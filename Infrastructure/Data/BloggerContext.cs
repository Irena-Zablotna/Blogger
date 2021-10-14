using Application.Identity;
using Application.Services;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
   public class BloggerContext:IdentityDbContext<ApplicationUser>
    {
        private readonly UserResolverService _userResolverService;

        public BloggerContext(DbContextOptions<BloggerContext> options, UserResolverService userResolverService) :base(options)
        {
            _userResolverService = userResolverService;
        }
        public DbSet<Post> Posts { get; set; }

        public async Task<int> SaveChangesAsync()
           
        {
            var entries = ChangeTracker.Entries()
                .Where(e=>e.Entity is AuditableEntity &&( e.State==EntityState.Added||e.State==EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((AuditableEntity)entityEntry.Entity).LastModified = DateTime.UtcNow;
                ((AuditableEntity)entityEntry.Entity).LastModifiedBy =_userResolverService.getUser();

                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).Created = DateTime.UtcNow;
                    ((AuditableEntity)entityEntry.Entity).CreatedBy = _userResolverService.getUser();
                }
            }
            return await base.SaveChangesAsync();
        }
    }
}
