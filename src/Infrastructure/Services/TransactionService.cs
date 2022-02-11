using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Models.Common;
using Application.Models.Transaction;
using Domain.Entities;
using MapsterMapper;

namespace Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<List<TransactionDto>> GetAsync(GetTransactionsRequest request)
        {
            var transactions = await _transactionRepository.GetAsync(request);
            return _mapper.Map<List<TransactionDto>>(transactions);
        }

        public async Task<PaginatedList<TransactionDto>> GetPagedResponseAsync(
            GetTransactionsWithPaginationRequest request)
        {
            var expenses = await _transactionRepository.GetPagedResponseAsync(request);
            return _mapper.Map<PaginatedList<TransactionDto>>(expenses);
        }

        public async Task<TransactionDto> GetByIdAsync(long id)
        {
            var expense = await _transactionRepository.GetByIdAsync(id);
            return _mapper.Map<TransactionDto>(expense);
        }

        public async Task<TransactionDto> AddAsync(CreateTransactionRequest request)
        {
            var entity = _mapper.Map<Transaction>(request);
            var created = await _transactionRepository.AddAsync(entity);
            return _mapper.Map<TransactionDto>(created);
        }

        public async Task UpdateAsync(TransactionDto request)
        {
            var entity = _mapper.Map<Transaction>(request);
            await _transactionRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _transactionRepository.DeleteAsync(id);
        }
    }
}