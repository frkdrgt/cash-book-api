using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBook.Services
{
    public class UserRegisterRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public Guid? GreenhouseSelector { get; set; }
    }


    public class UserLoginRequestDto
    {
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
    }



    public class UserUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public Guid? GreenhouseSelector { get; set; }
    }
}
