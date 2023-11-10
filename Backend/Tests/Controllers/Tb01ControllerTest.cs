using API.Controllers;
using Application.DTOs;
using Application.Interfaces;
using Application.Response;
using Application.Services;
using Domain.Pagination;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Controllers
{
    public class Tb01ControllerTest
    {
        private readonly Tb01Controller controller;
        private readonly Mock<ITb01Service> mockService;
        private readonly Mocks mocks;

        public Tb01ControllerTest()
        {
            mockService = new Mock<ITb01Service>();
            controller = new Tb01Controller(mockService.Object);
            mocks = new Mocks();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllRecords()
        {
            var paginationParametersDTO = new PaginationParametersDTO();
            var result = await controller.GetAll(paginationParametersDTO);
            Assert.IsType<ActionResult<PagedList<Tb01DTO>>>(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnRecordByIdPassed()
        {
            var mockTb01DTO = mocks.MockTb01DTO();

            mockService.Setup(service => service.GetById(mockTb01DTO.Id))
                       .ReturnsAsync(mockTb01DTO);

            var result = await controller.GetById(mockTb01DTO.Id);
            Assert.IsType<ActionResult<Tb01DTO>>(result);

            var okObjectResult = result.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var tb01Dto = okObjectResult.Value as Tb01DTO;
            Assert.NotNull(tb01Dto);

            Assert.Equal(mockTb01DTO.Id, tb01Dto.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound()
        {
            var mockTb01DTO = mocks.MockTb01DTO();

            mockService.Setup(service => service.GetById(mockTb01DTO.Id))
                       .ReturnsAsync((Tb01DTO) null);

            var result = await controller.GetById(mockTb01DTO.Id);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtRouteResult()
        {
            var mockTb01DTO = mocks.MockTb01DTO();

            mockService.Setup(service => service.Create(mockTb01DTO))
                .ReturnsAsync(new MessageResponse(true, "Registro criado com sucesso!"));

            var result = await controller.Create(mockTb01DTO);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<MessageResponse>>(result);
            var createdAtRouteResult = result.Result as CreatedAtRouteResult;
            Assert.NotNull(createdAtRouteResult);
            Assert.Equal("GetById", createdAtRouteResult.RouteName);
            Assert.Equal(mockTb01DTO.Id, createdAtRouteResult.RouteValues["id"]);
            Assert.Equal(mockTb01DTO, createdAtRouteResult.Value);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest()
        {
            var mockTb01DTO = mocks.MockTb01DTO();

            mockService.Setup(service => service.Create(mockTb01DTO))
                .Throws(new Exception());

            var result = await controller.Create(mockTb01DTO);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<MessageResponse>>(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Update_ShouldReturnOk()
        {
            var mockTb01DTO = mocks.MockTb01DTO();

            mockService.Setup(service => service.Update(mockTb01DTO))
                .ReturnsAsync(new MessageResponse(true, "Registro atualizado com sucesso!"));

            var result = await controller.Update(mockTb01DTO);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<MessageResponse>>(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var okObjectResult = result.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var returnedObject = okObjectResult.Value;
            Assert.Equal(mockTb01DTO, returnedObject);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest()
        {
            var mockTb01DTO = mocks.MockTb01DTO();

            mockService.Setup(service => service.Update(mockTb01DTO))
                .Throws(new Exception("O registro não existe"));

            var result = await controller.Update(mockTb01DTO);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<MessageResponse>>(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent()
        {
            var mockTb01DTO = mocks.MockTb01DTO();

            mockService.Setup(service => service.Delete(mockTb01DTO))
                .ReturnsAsync(new MessageResponse(true, "Registro deletado com sucesso!"));

            var result = await controller.Delete(mockTb01DTO);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<MessageResponse>>(result);
            Assert.IsType<NoContentResult>(result.Result);

            var noContentObjectResult = result.Result as NoContentResult;
            Assert.NotNull(noContentObjectResult);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound()
        {
            var mockTb01DTO = mocks.MockTb01DTO();

            mockService.Setup(service => service.Delete(mockTb01DTO))
                .ReturnsAsync(new MessageResponse(false, "O registro não existe"));

            var result = await controller.Delete(mockTb01DTO);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<MessageResponse>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Delete_ShouldReturnBadRequest()
        {
            var mockTb01DTO = mocks.MockTb01DTO();

            mockService.Setup(service => service.Delete(mockTb01DTO))
                .Throws(new Exception());

            var result = await controller.Delete(mockTb01DTO);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<MessageResponse>>(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
