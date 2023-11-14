using Application.DTOs;
using Application.Interfaces;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class Tb01Service : ITb01Service
    {
        private readonly ITb01Repository _repository;
        private readonly IMapper _mapper;

        public Tb01Service(ITb01Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedList<Tb01DTO>> GetAll(PaginationParametersDTO paginationParametersDTO)
        {
            var paginationParametersEntity = _mapper.Map<PaginationParameters>(paginationParametersDTO);
            var tb01Entity = await _repository.GetAll(paginationParametersEntity);
            return _mapper.Map<PagedList<Tb01DTO>>(tb01Entity);
        }

        public async Task<Tb01DTO> GetById(int id)
        {
            if (await _repository.Exists(id))
            {
                var response = await _repository.GetById(id);
                return _mapper.Map<Tb01DTO>(response);
            }
            else
            {
                return null;
            }
        }

        public async Task<int> Count()
        {
            var response = await _repository.Count();
            return response;
        }

        public async Task<MessageResponse> Create(Tb01DTO tb01DTO)
        {
            try
            {
                tb01DTO.ColDt = DateTime.Now;
                var tb01Entity = _mapper.Map<Tb01>(tb01DTO);
                await _repository.Create(tb01Entity);
                return new MessageResponse(true, "Registro criado com sucesso!");
            }
            catch (Exception ex)
            {
                return new MessageResponse(false, ex.Message);
            }
        }

        public async Task<MessageResponse> Update(Tb01DTO tb01DTO)
        {
            try
            {
                var exists = await _repository.Exists(tb01DTO.Id);
                if (!exists)
                {
                    throw new Exception("O registro não existe");
                }
                tb01DTO.ColDt = DateTime.Now;
                var tb01Entity = _mapper.Map<Tb01>(tb01DTO);
                await _repository.Update(tb01Entity);
                return new MessageResponse(true, "Registro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return new MessageResponse(false, ex.Message);
            }
        }

        public async Task<MessageResponse> Delete(Tb01DTO tb01DTO)
        {
            try
            {
                var exists = await _repository.Exists(tb01DTO.Id);
                if (!exists)
                {
                    return new MessageResponse(false, "O registro não existe");
                }
                var tb01Entity = _mapper.Map<Tb01>(tb01DTO);
                await _repository.Delete(tb01Entity.Id);
                return new MessageResponse(true, "Registro deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return new MessageResponse(false, ex.Message);
            }
        }
    }
}
