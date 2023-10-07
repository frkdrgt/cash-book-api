using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBook.Data
{
    public class Warehouse : EntityBase<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SupervisorName { get; set; }
        public int CompanyId { get; set; }
    }
}
