using AutoMapper;
using CleanArchMvc.Applications.DTOs;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Applications.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}