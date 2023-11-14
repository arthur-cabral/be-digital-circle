using Domain.Entities;
using Domain.Pagination;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class Tb01Repository : RepositoryBase, ITb01Repository
    {
        public Tb01Repository([NotNull] AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Tb01>> GetAll(PaginationParameters paginationParameters)
        {
            const string sql = @"select
                       id,
                       col_texto as ColTexto,
                       col_dt as ColDt
                   from TB01
                   order by col_dt desc 
                   limit @limit offset @offset";

            var result = await Connection.QueryAsync<Tb01>(sql, new { limit = paginationParameters.PageSize, offset = paginationParameters.PageNumber * paginationParameters.PageSize }, Transaction);
            return PagedList<Tb01>.ToPagedList(
                result.AsQueryable(),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );
        }

        public async Task<Tb01> GetById(int id)
        {
            const string sql = @"select
                               id,
                               col_texto as ColTexto,
                               col_dt as ColDt
                               from tb01
                               where id = @id";

            return await Connection.QueryFirstOrDefaultAsync<Tb01>(sql, new { id }, Transaction);
        }

        public async Task<int> Count()
        {
            const string sql = @"select
                               count(*) 
                               from tb01";

            return await Connection.ExecuteScalarAsync<int>(sql, Transaction);
        }

        public async Task Create(Tb01 tb01)
        {
            string sql = @"insert into TB01
                                 (col_texto,
                                 col_dt)
                                 values
                                 (@ColTexto, 
                                 @ColDt)";

            await Connection.ExecuteAsync(sql, new { tb01.ColTexto, tb01.ColDt }, Transaction);
        }

        public async Task Update(Tb01 tb01)
        {
            string sql = @"update TB01
                                 set col_texto = @ColTexto,
                                 col_dt = @ColDt
                                 where id = @Id";

            await Connection.ExecuteAsync(sql, new { tb01.ColTexto, tb01.ColDt, tb01.Id }, Transaction);
        }

        public async Task Delete(int id)
        {
            string sql = @"delete from TB01 where id = @id";

            await Connection.ExecuteAsync(sql, new { id }, Transaction);
        }

        public async Task<bool> Exists(int id)
        {
            const string sql = @"select count(*) from TB01 where id = @id";
            return await Connection.ExecuteScalarAsync<bool>(sql, new { id }, Transaction);
        }
    }
}
