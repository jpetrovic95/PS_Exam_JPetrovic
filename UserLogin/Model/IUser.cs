using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin.Model
{
    public interface IUser
    {
        string Username { get; set; }
        string Password { get; set; }
        string FacultyNumber { get; set; }
        UserRoles UserRole { get; set; }
        DateTime Created { get; }
        DateTime ActiveTo { get; set; }
        string ToString();
    }
}
