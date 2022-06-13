using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTickets.model
{
    public enum UserType
    {
        Manager,
        Client
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Email { get; set; }

        public string Password { get; set; }

        public UserType UserType { get; set; }

        public User() { }

        public override string ToString()
        {
            return Email;
        }

        public User(string firstName, string lastName, string email, string password, UserType userType)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            UserType = userType;
        }
    }
}
