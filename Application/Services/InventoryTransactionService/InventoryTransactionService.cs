using Application.Repositories;
using Application.Services.InventoryTransactionService.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.InventoryTransactionService
{
    public class InventoryTransactionService : IInventoryTransactionService
    {
        private readonly IGenericRepository<InventoryTransaction> _inventoryTransactionRepository;
        private readonly IGenericRepository<Product> _productRepository;
        public InventoryTransactionService(IGenericRepository<InventoryTransaction> inventoryTransactionRepository, IGenericRepository<Product> productRepository)
        {
            _inventoryTransactionRepository = inventoryTransactionRepository;
            _productRepository = productRepository;
        }

        public async Task CreateTransaction(CreateTransactionDto input)
        {

            if (input.Quantity <= 0)
                throw new Exception("CurrentStock should be graeter than quantity");


            var product = await _productRepository.GetByIdAsync(input.ProductId);

            if (input.TransactionType == TransactionType.StockIn)
            {
                product.CurrentStock += input.Quantity;
                _productRepository.Update(product);
            }
            else
            {
                if (product.CurrentStock < input.Quantity)
                    throw new Exception("CurrentStock should be graeter than quantity");

                product.CurrentStock -= input.Quantity;
                _productRepository.Update(product);
            }

            var data = new InventoryTransaction
            {
                ProductId = input.ProductId,
                Quantity = input.Quantity,
                TransactionType = input.TransactionType,
                CreateAt = DateTime.UtcNow,
            };

            await _inventoryTransactionRepository.InsertAsync(data);
            await _inventoryTransactionRepository.SaveChangesAsync();
        }

        public async Task<List<GetAllTransactionsDto>> GetAllTransactions()
        {
            var data =  _inventoryTransactionRepository.GetAll().Include(x => x.Product);

            var result = await data.Select(x => new GetAllTransactionsDto
            {
                Id = x.Id,
                ProductName = x.Product.Name,
                Quantity = x.Quantity,
                CreateAt = x.CreateAt,
                TransactionType = x.TransactionType
            }).ToListAsync();

            return result;
        }

        public async Task<GetTransactionByIdDto> GetTransactionById(Guid id)
        {
            var data = await _inventoryTransactionRepository.GetAll().Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                new Exception("Inventory Transaction not found.");

            var result = new GetTransactionByIdDto
            {
                Id = data.Id,
                ProductName = data.Product.Name,
                Quantity = data.Quantity,
                CreateAt = data.CreateAt,
                TransactionType = data.TransactionType
            };
            return result;
        }

    }
}
