using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<PaginationParameters, PaginationParametersDTO>().ReverseMap();
            CreateMap<Tb01, Tb01DTO>().ReverseMap();
        }
    }
}
