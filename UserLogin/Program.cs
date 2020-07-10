using System;
using static System.Console;
using System.Collections.Generic;
using UserLogin.Controller;

namespace UserLogin
{
    class Program
    {
        static void Main(string[] args)
        {
            UserLoginController controller = new UserLoginController();
            controller.Run();
        }
    }
}
