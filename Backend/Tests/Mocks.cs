using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Mocks
    {
        public Mocks() { }

        public Tb01DTO MockTb01DTO()
        {
            return new()
            {
                Id = 1,
                ColTexto = "test",
                ColDt = DateTime.Now
            };
        }

        public Tb01 MockTb01()
        {
            return new()
            {
                Id = 1,
                ColTexto = "test",
                ColDt = DateTime.Now
            };
        }
    }
}
