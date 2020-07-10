using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLogin.Model;

namespace UserLogin.View
{
    class LoginView : IView
    {
        public void ShowMenu()
        {
            Console.WriteLine(
                "*** ADMIN MENU *** \n" +
                "Your choice: \n" +
                "0: Exit \n" +
                "1: Change user role \n" +
                "2: Change user 'active to' date \n" +
                "3: List all users \n" +
                "4: Print current session activity \n" +
                "5: Print all activities (from file) \n");
            Console.ReadLine();
        }

        public void PrintErrorLine(string errorMsg)
        {
            PrintMsgLine($"ERROR: {errorMsg}");
        }
        public void PrintMsgLine(string msg = "")
        {
            PrintMsg(msg + '\n');
        }
        public void PrintUser(IUser user)
        {
            PrintMsgLine(user.ToString());
        }

        public void PrintAllUsers(IUserData userData)
        {
            foreach (IUser user in userData.GetUsersData())
            {
                PrintUser(user);
            }
        }

        public void PrintMsg(string msg)
        {
            Console.Write(msg);
        }
    }
}
