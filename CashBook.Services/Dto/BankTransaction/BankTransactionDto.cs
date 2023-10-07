using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBook.Services
{
    public class BankTransactionGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int TransactionType { get; set; }
        public Guid BankId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

    }
    public class BankTransactionCreateRequestDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int TransactionType { get; set; }
        public Guid BankId { get; set; }
        public DateTime? CreateDate { get; set; }
    }

    public class BankTransactionUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int TransactionType { get; set; }
        public Guid BankId { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}