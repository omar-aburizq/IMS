using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.InventoryTransactionService.DTOs
{
    public class GetTransactionByIdDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateAt { get; set; }
        public TransactionType TransactionType { get; set; }
        public string TypeStr
        {
            get
            {
                return TransactionType.ToString();
            }
        }

    }
}
