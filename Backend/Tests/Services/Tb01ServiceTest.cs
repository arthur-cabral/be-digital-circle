using API.Controllers;
using Application.DTOs;
using Application.Interfaces;
using Application.Response;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Pagination;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Services
{
    public class Tb01ServiceTest
    {
        private readonly Tb01Service service;
        private readonly Mock<ITb01Repository> mockRepository;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mocks mocks;

        public Tb01ServiceTest()
        {
            mockRepository = new Mock<ITb01Repository>();
            mockMapper = new Mock<IMapper>();
            service = new Tb01Service(mockRepository.Object, mockMapper.Object);
            mocks = new Mocks();
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfDTO()
        {
            var paginationParametersDTO = new PaginationParametersDTO
            {
                PageSize = 10,
                PageNumber = 1
            };

            var paginationParametersEntity = new PaginationParameters
            {
                PageSize = paginationParametersDTO.PageSize,
                PageNumber = paginationParametersDTO.PageNumber
            };

            var mockTb01 = mocks.MockTb01();
            var tb01EntityList = new List<Tb01>
            {
                mockTb01
            };

            var pagedList = PagedList<Tb01>.ToPagedList(tb01EntityList.AsQueryable(), paginationParametersEntity.PageNumber, paginationParametersEntity.PageSize);

            mockRepository.Setup(r => r.GetAll(It.IsAny<PaginationParameters>())).ReturnsAsync(pagedList);

            mockMapper.Setup(m => m.Map<PaginationParameters>(paginationParametersDTO)).Returns(paginationParametersEntity);
            mockMapper.Setup(m => m.Map<PagedList<Tb01DTO>>(It.IsAny<PagedList<Tb01>>())).Returns((PagedList<Tb01> pagedList) =>
            {
                var tb01DTOList = pagedList.Select(tb01 => new Tb01DTO { Id = tb01.Id, ColTexto = tb01.ColTexto, ColDt = tb01.ColDt }).ToList();
                return PagedList<Tb01DTO>.ToPagedList(tb01DTOList.AsQueryable(), pagedList.CurrentPage, pagedList.PageSize);
            });

            var result = await service.GetAll(paginationParametersDTO);

            Assert.NotNull(result);
            Assert.IsType<PagedList<Tb01DTO>>(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnRecordByIdPassed()
        {
            var mockTb01 = mocks.MockTb01();
            var mockTb01DTO = mocks.MockTb01DTO();

            mockRepository.Setup(r => r.Exists(mockTb01.Id)).ReturnsAsync(true);

            mockRepository.Setup(r => r.GetById(mockTb01.Id)).ReturnsAsync(mockTb01);

            mockMapper.Setup(m => m.Map<Tb01DTO>(mockTb01)).Returns(mockTb01DTO);


            var result = await service.GetById(mockTb01DTO.Id);
            Assert.IsType<Tb01DTO>(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull()
        {
            var mockTb01 = mocks.MockTb01();
            var mockTb01DTO = mocks.MockTb01DTO();

            mockRepository.Setup(r => r.Exists(mockTb01.Id)).ReturnsAsync(false);

            var result = await service.GetById(mockTb01DTO.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task Count_ShouldReturnAllRecords()
        {
            var mockTb01 = mocks.MockTb01();
            var mockTb01DTO = mocks.MockTb01DTO();

            mockRepository.Setup(r => r.Count()).ReturnsAsync(1);

            var result = await service.Count();
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Create_ShouldReturnResponseMessageSuccessTrue()
        {
            var mockTb01 = mocks.MockTb01();
            var mockTb01DTO = mocks.MockTb01DTO();

            mockRepository.Setup(r => r.Create(mockTb01));

            mockMapper.Setup(m => m.Map<Tb01DTO>(mockTb01)).Returns(mockTb01DTO);

            var result = await service.Create(mockTb01DTO);
            Assert.IsType<MessageResponse>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Update_ShouldReturnResponseMessageSuccessTrue()
        {
            var mockTb01 = mocks.MockTb01();
            var mockTb01DTO = mocks.MockTb01DTO();

            mockRepository.Setup(r => r.Exists(mockTb01.Id).Result).Returns(true);

            mockRepository.Setup(r => r.Update(mockTb01));

            mockMapper.Setup(m => m.Map<Tb01DTO>(mockTb01)).Returns(mockTb01DTO);

            var result = await service.Update(mockTb01DTO);
            Assert.IsType<MessageResponse>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Update_ShouldReturnResponseMessageSuccessFalse()
        {
            var mockTb01 = mocks.MockTb01();
            var mockTb01DTO = mocks.MockTb01DTO();

            mockRepository.Setup(r => r.Exists(mockTb01.Id).Result).Returns(false);

            var result = await service.Update(mockTb01DTO);
            Assert.IsType<MessageResponse>(result);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task Delete_ShouldReturnResponseMessageSuccessTrue()
        {
            var mockTb01 = mocks.MockTb01();
            var mockTb01DTO = mocks.MockTb01DTO();

            mockRepository.Setup(r => r.Exists(mockTb01.Id).Result).Returns(true);

            mockMapper.Setup(m => m.Map<Tb01>(mockTb01DTO)).Returns(mockTb01);

            mockRepository.Setup(r => r.Delete(mockTb01.Id));

            var result = await service.Delete(mockTb01DTO);
            Assert.IsType<MessageResponse>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Delete_ShouldReturnResponseMessageSuccessFalse()
        {
            var mockTb01 = mocks.MockTb01();
            var mockTb01DTO = mocks.MockTb01DTO();

            mockRepository.Setup(r => r.Exists(mockTb01.Id).Result).Returns(false);

            var result = await service.Delete(mockTb01DTO);
            Assert.IsType<MessageResponse>(result);
            Assert.False(result.Success);
        }
    }
}
