using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public sealed class AppDbContext : IDisposable
    {
        private readonly Guid _id;

        public IDbConnection Connection { get; }

        public IDbTransaction Transaction { get; set; }

        public AppDbContext()
        {
            try
            {
                this._id = Guid.NewGuid();
                //this.Connection = new SQLiteConnection($"Data Source=D:\\SQLiteDbs\\db_desafio.sdb; Version=3;");
                this.Connection = new SQLiteConnection($"Data Source=..\\..\\db_desafio.sdb; Version=3;");
                this.Connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante a inicialização do banco de dados: {ex.Message}");
                throw;
            }
        }

        public void Dispose() => this.Connection?.Dispose();
    }
}
