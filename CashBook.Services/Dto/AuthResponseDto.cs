using System;
using System.Collections.Generic;
using System.Text;

namespace CashBook.Services
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
    }
}
