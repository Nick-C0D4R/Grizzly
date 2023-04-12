using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class UserDTO : IDTOEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }

        public override string ToString()
        {
            return $"Name: {UserName} | Surname: {UserSurname} | Role id: {RoleId} | " +
                $"Login: {Login} | Phone number: {PhoneNumber} | Joined: {JoinDate}";
        }
    }
}
