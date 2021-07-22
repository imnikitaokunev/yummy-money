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
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<List<ExpenseDto>> GetAsync(GetExpensesRequest request)
        {
            var expenses = await _expenseRepository.GetAsync(request);
            return _mapper.Map<List<ExpenseDto>>(expenses);
        }

        public async Task<List<ExpenseWithCategoryDto>> GetWithCategoryAsync(GetExpensesRequest request)
        {
            var expenses = await _expenseRepository.GetWithCategoryAsync(request);
            return _mapper.Map<List<ExpenseWithCategoryDto>>(expenses);
        }

        public async Task<PaginatedList<ExpenseDto>> GetPagedResponseAsync(GetExpensesWithPaginationRequest request)
        {
            var expenses = await _expenseRepository.GetPagedResponseAsync(request);
            return _mapper.Map<PaginatedList<ExpenseDto>>(expenses);
        }

        public async Task<ExpenseDto> GetByIdAsync(long id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            return _mapper.Map<ExpenseDto>(expense);
        }

        public async Task<ExpenseDto> AddAsync(CreateExpenseRequest expense)
        {
            var entity = _mapper.Map<Expense>(expense);
            var created = await _expenseRepository.AddAsync(entity);
            return _mapper.Map<ExpenseDto>(created);
        }

        public async Task UpdateAsync(ExpenseDto expense)
        {
            var entity = _mapper.Map<Expense>(expense);
            await _expenseRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _expenseRepository.DeleteAsync(id);
        }
    }
}
