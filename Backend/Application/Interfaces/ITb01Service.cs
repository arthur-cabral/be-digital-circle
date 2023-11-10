using Application.DTOs;
using Application.Response;
using Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITb01Service
    {
        Task<PagedList<Tb01DTO>> GetAll(PaginationParametersDTO paginationParametersDTO);
        Task<Tb01DTO> GetById(int id);
        Task<MessageResponse> Create(Tb01DTO tb01);
        Task<MessageResponse> Update(Tb01DTO tb01);
        Task<MessageResponse> Delete(Tb01DTO tb01);
    }
}
