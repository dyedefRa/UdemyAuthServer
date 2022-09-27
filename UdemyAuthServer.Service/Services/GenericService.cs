using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UdemyAuthServer.Core.Repositories;
using UdemyAuthServer.Core.Services;
using UdemyAuthServer.Core.UnitOfWork;

namespace UdemyAuthServer.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<TDto>> AddAsync(TDto dto)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(dto);
            await _genericRepository.AddAsync(newEntity);

            await _unitOfWork.CommitAsync();
            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);

            return Response<TDto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var list = await _genericRepository.GetAllAsync();
            var products = ObjectMapper.Mapper.Map<List<TDto>>(list);

            return Response<IEnumerable<TDto>>.Success(products, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return Response<TDto>.Fail("Id not found", 404, true);
            }
            var dto = ObjectMapper.Mapper.Map<TDto>(entity);

            return Response<TDto>.Success(dto, 200);
        }

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return Response<NoDataDto>.Fail("Id not found", 404, true);
            }
            _genericRepository.Remove(entity);

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> Update(int id, TDto dto)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return Response<NoDataDto>.Fail("Id not found", 404, true);
            }
            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            _genericRepository.Update(updateEntity);

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = await _genericRepository.Where(predicate).ToListAsync();
            //ToListAsync demeden önce IQueryable da isteğimiz kadar sorgu ekleyebiliriz. 
            //ToListAsync dedikten sonra dataları getirir.
            var dtoList = ObjectMapper.Mapper.Map<IEnumerable<TDto>>(list);

            return Response<IEnumerable<TDto>>.Success(dtoList, 200);
        }
    }
}
