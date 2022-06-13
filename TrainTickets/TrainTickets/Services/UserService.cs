using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.Database;
using TrainTickets.dto;
using TrainTickets.model;

namespace TrainTickets.Services
{
    public class UserService
    {
        public User logUser { get; set; }
        public User findByEmail(string email)
        {
            if (email == null) return null;

            using (var db = new DatabaseContext())
            {
                foreach (var user in db.Users)
                {
                    if (email == user.Email)
                    {

                        return user;
                    }
                }
            }
            return null;
        }


        public bool Login(string email, string password)
        {
            User user = findByEmail(email);
            if (user == null) return false;

            if (user.Password == password)
            {
                logUser = user;
                Console.WriteLine("User successfully logged in: " + email);

                return true;
            }

            return false;
        }

        public bool Register(RegisterUserDTO user)
        {
            try
            {
                User u = findByEmail(user.Email);
                if (u != null) return false;

                User newUser = new User(user.FirstName, user.LastName, user.Email, user.Password, UserType.Client);
                using (var db = new DatabaseContext())
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return true;
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
