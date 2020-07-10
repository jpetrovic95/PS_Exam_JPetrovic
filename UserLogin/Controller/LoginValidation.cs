using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLogin.Model;

namespace UserLogin.Controller
{
    class LoginValidation
    {
        private readonly string username;
        private readonly string password;
        private string errMessage;
        private readonly ActionOnError actionOnError;
		public delegate void ActionOnError(string errorMsg);
        public static string currentUserUsername;
        public static UserRoles currentUserRole { get; private set; }

        public LoginValidation(string username, string password, ActionOnError actionOnError)
        {
            this.username = username;
            this.password = password;
            this.actionOnError = actionOnError;
        }

        public bool ValidateUserInput(ref IUser user, IUserData userData)
        {
            if (username.Equals(String.Empty) == true)
            {
                errMessage = "Please enter the Username. Username cannot be empty.";
                actionOnError(errMessage);
                return false;
            }

            if (username.Length < 5)
            {
                errMessage = "Username cannot be less than 5 characters.";
                actionOnError(errMessage);
                return false;
            }

            if (password.Equals(String.Empty) == true)
            {
                errMessage = "Please enter the password again. Password cannot be empty.";
                actionOnError(errMessage);
                return false;
            }

            if (password.Length < 5)
            {
                errMessage = "The password is less than 5 characters";
                actionOnError(errMessage);
                return false;
            }

            user = userData.IsUserPassCorrect(username, password);

            if (user == null)
            {
                errMessage = $"There is no such user found: {username}. Please try again. ";
                actionOnError(errMessage);
                return false;
            }

            currentUserUsername = user.Username;
            currentUserUsername = user.UserRole.ToString();

            Logger.LogActivity($"Successful login of user {user.Username}", true);

            return true;
        }
    }
}
