using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrossCuttingFunctionality.FilterModels;

namespace BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public CommentService(IUnitOfWork unit, IMapper mapper)
        {
            _mapper = mapper;
            _unit = unit;
        }
        public async Task<int> Create(CommentDto comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            var temp = _mapper.Map<Comment>(comment);
            var resultComment = await _unit.CommentRepository.Add(temp);
            await _unit.Commit();
            return resultComment.CommentId;
        }

        public async Task Edit(CommentDto comment)
        {
            if (comment.CommentId < 0)
                throw new ArgumentException("Id can not be less than 0.");
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            try
            {
                await _unit.CommentRepository.Update(comment.CommentId, _mapper.Map<Comment>(comment));
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
            await _unit.CommentRepository.Delete(id);
            await _unit.Commit();
        }

        public async Task<CommentDto> GetById(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            var result = await _unit.CommentRepository.GetById(id);
            if (result == null)
                throw new EntryNotFoundException($"Entry with id = {id} does not found");
            return _mapper.Map<CommentDto>(result);
        }

        public async Task<IEnumerable<CommentDto>> GetAll()
        {
            var result = await _unit.CommentRepository.GetAll();
            return _mapper.Map<IEnumerable<CommentDto>>(result);
        }

        public async Task<IList<CommentDto>> GetCommentsWithFilters(int gameId, FilterModel filter)
        {
            if (gameId < 0)
                throw new ArgumentException("Id can not be less than 0");
            var result = await _unit.CommentRepository.GetCommentsWithFilters(gameId, filter);
            return _mapper.Map<IList<CommentDto>>(result);
        }
    }
}
