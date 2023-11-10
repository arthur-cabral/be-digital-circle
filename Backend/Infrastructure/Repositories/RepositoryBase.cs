using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Context;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public abstract class RepositoryBase : IRepository
    {
        private readonly AppDbContext _context;

        public IDbConnection Connection => this._context.Connection;

        public IDbTransaction Transaction
        {
            get => this._context.Transaction;
            set => this._context.Transaction = value;
        }

        protected RepositoryBase([NotNull] AppDbContext context)
        {
            this._context = context;
        }
    }
}
