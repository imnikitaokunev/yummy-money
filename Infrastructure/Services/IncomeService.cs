using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.Models;
using Application.Models;
using Domain.Entities;
using MapsterMapper;

namespace Infrastructure.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IMapper _mapper;

        public IncomeService(IIncomeRepository incomeRepository, IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _mapper = mapper;
        }

        public async Task<List<IncomeDto>> GetAsync(GetIncomesRequest request)
        {
            var incomes = await _incomeRepository.GetAsync(request);
            return _mapper.Map<List<IncomeDto>>(incomes);
        }

        public async Task<PaginatedList<IncomeDto>> GetPagedResponseAsync(GetIncomesWithPaginationRequest request)
        {
            var incomes = await _incomeRepository.GetPagedResponseAsync(request);
            return _mapper.Map<PaginatedList<IncomeDto>>(incomes);
        }

        public async Task<IncomeDto> GetByIdAsync(long id)
        {
            var income = await _incomeRepository.GetByIdAsync(id);
            return _mapper.Map<IncomeDto>(income);
        }

        public async Task<IncomeDto> AddAsync(CreateIncomeRequest income)
        {
            var entity = _mapper.Map<Income>(income);
            var created = await _incomeRepository.AddAsync(entity);
            return _mapper.Map<IncomeDto>(created);
        }

        public async Task UpdateAsync(IncomeDto income)
        {
            var entity = _mapper.Map<Income>(income);
            await _incomeRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _incomeRepository.DeleteAsync(id);
        }
    }
}
