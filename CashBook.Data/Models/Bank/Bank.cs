using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBook.Data
{
    public class Bank : EntityBase<Guid>
    {
        public string Name { get; set; }
        public int CompanyId { get; set; }
    }
}