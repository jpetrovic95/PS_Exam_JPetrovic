using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin.Model
{
    public class User : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FacultyNumber { get; set; }
        public UserRoles UserRole { get; set; }
        public DateTime Created { get; }
        public DateTime ActiveTo { get; set; }

        public User() { }

        public User(string name, string pass, string facNumber, UserRoles role)
        {
            Username = name;
            Password = pass;
            FacultyNumber = facNumber;
            UserRole = role;
            Created = DateTime.Now;
            ActiveTo = DateTime.Now.AddYears(5);
        }
        public override string ToString()
        {
            return $"Username: {Username}\n" +
                $"Password: {Password}\n" +
                $"Faculty number: {FacultyNumber}\n" +
                $"Role: {UserRole}\n" +
                $"Date Created: {Created}\n";
        }
    }
}
