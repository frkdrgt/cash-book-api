using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBook.Services
{
    public class WarehouseGetDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SupervisorName { get; set; }
        public int CompanyId { get; set; }
    }

    public class WarehouseCreateDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SupervisorName { get; set; }
        public int CompanyId { get; set; }
    }

    public class WarehouseUpdateDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SupervisorName { get; set; }
        public int CompanyId { get; set; }
    }
}