using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLogin.Model;

namespace UserLogin.View
{
    public interface IView
    {
        void ShowMenu();
        void PrintAllUsers(IUserData userData);
    }
}
