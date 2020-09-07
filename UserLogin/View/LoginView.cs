using System;
using UserLogin.Model;

namespace UserLogin.View
{
    class LoginView : IView
    {
        public void ShowMenu()
        {
            Console.WriteLine(
                "Options: \n" +
                "0: Exit \n" +
                "1: Change user role \n" +
                "2: Change user activity \n" +
                "3: List all users \n" +
                "4: Print current log activity \n" +
                "5: Print all log activities \n");
        }

        public void PrintAllUsers(IUserData userData)
        {
            foreach (IUser user in userData.GetUsersData())
            {
                Console.WriteLine(user.ToString());
            }
        }
		
		public void PrintError(string errorMsg)
        {
            Console.WriteLine($"ERROR: {errorMsg}");
        }
		
        public void PrintMessage(string msg)
        {
            Console.Write(msg);
        }
    }
}
