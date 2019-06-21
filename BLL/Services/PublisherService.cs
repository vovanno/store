using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Interfaces;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public PublisherService(IUnitOfWork unit, IMapper mapper)
        {
            _mapper = mapper;
            _unit = unit;
        }
        public async Task<int> Create(PublisherDto publisher)
        {
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));
            var temp = _mapper.Map<Publisher>(publisher);
            var resultPublisher = await _unit.PublisherRepository.Add(temp);
            await _unit.Commit();
            return resultPublisher.PublisherId;
        }

        public async Task Edit(PublisherDto publisher)
        {
            if (publisher.PublisherId < 0)
                throw new ArgumentException("Id can not be less than 0.");
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));
            try
            {
                await _unit.PublisherRepository.Update(publisher.PublisherId, _mapper.Map<Publisher>(publisher));
            }
            catch (EntryNotFoundException e)
            {
                throw new ArgumentException("Entry does not found.", e);
            }
            await _unit.Commit();
        }

        public async Task Delete(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            await _unit.PublisherRepository.Delete(id);
            await _unit.Commit();
        }

        public async Task<PublisherDto> GetById(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            var result = await _unit.PublisherRepository.GetById(id);
            if (result == null)
                throw new EntryNotFoundException($"Entry with id = {id} does not found");
            return _mapper.Map<PublisherDto>(result);
        }

        public async Task<IEnumerable<PublisherDto>> GetAll()
        {
            var result = await _unit.PublisherRepository.GetAll();
            return _mapper.Map<IEnumerable<PublisherDto>>(result);
        }

        public async Task<IList<GameDto>> GetGamesByPublisherId(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            var result = await _unit.PublisherRepository.GetGamesByPublisherId(id);
            return _mapper.Map<IList<GameDto>>(result);
        }
    }
}
