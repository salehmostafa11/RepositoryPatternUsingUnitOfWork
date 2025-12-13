using AutoMapper;
using RepositoryPatternUsingUOW.Core.DTOs.Authors;
using RepositoryPatternUsingUOW.Core.DTOs.Books;
using RepositoryPatternUsingUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternUsingUOW.Core.Assets
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBook, Book>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<Book, CreateBook>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<GetBook, Book>().ForMember(dest => dest.Name, opt=> opt.MapFrom(src=>src.Name))
                .ForPath(dest => dest.Author.Name, opt => opt.MapFrom(src => src.AuthorName)); // ForPath Only used for nested properties
            CreateMap<Book, GetBook>().ForMember(dest => dest.Name, opt=> opt.MapFrom(src=>src.Name))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));


            CreateMap<GetAuthorDto, Author>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<Author, GetAuthorDto>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<UpdateAuthorDto, Author>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest=>dest.Id, opt=>opt.Ignore());
            CreateMap<Author, UpdateAuthorDto>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Author, CreateAuthorDto>();
            CreateMap<CreateAuthorDto, Author>();
        }
    }
}
