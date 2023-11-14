using Domain.Entities;
using Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITb01Repository
    {
        Task<PagedList<Tb01>> GetAll(PaginationParameters paginationParameters);
        Task<Tb01> GetById(int id);
        Task<int> Count();
        Task Create(Tb01 tb01);
        Task Update(Tb01 tb01);
        Task Delete(int id);
        Task<bool> Exists(int id);
    }
}
