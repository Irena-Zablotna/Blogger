using Application.Dto;
using Application.DTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
   public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDto>()
               .ForMember(dest=>dest.CreationDate, opt=>opt.MapFrom(src=>src.Created));
            CreateMap<CreatePostDto, Post>();
            CreateMap<UpdatePostDto, Post>();
            
        }
    }
}
