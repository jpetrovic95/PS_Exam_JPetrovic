using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace UserLogin.Model
{
    public class UserData : IUserData
    {
        private List<IUser> _testUser = new List<IUser>();

        public UserData()
		{
			ResetTestUserData();
		}

        public void ResetTestUserData()
        {
            IUser user1 = new User("JPetrovic", "pass1", "123214003", UserRoles.ADMIN);
            IUser user2 = new User("SomeName", "pass2", "123456789", UserRoles.STUDENT);
            IUser user3 = new User("Administrator", "pass3", "987654321", UserRoles.ADMIN);

            user1.ActiveTo = DateTime.Now.AddYears(2000);
            user2.ActiveTo = DateTime.Now.AddYears(2000);
            user3.ActiveTo = DateTime.Now.AddYears(2000);

            _testUser.Add(user1);
            _testUser.Add(user2);
            _testUser.Add(user3);
        }

        public IReadOnlyList<IUser> GetUsersData()
        {
            if (_testUser.Count == 0) ResetTestUserData();
            return _testUser;
        }

        public IUser IsUserPassCorrect(string username, string password)
        {
            return _testUser.FirstOrDefault(user => user.Username == username && user.Password == password);
        }

        public void SetUserActiveTo(int usernameIndex, DateTime newActiveTo)
        {
            Logger.LogActivity($"Changing user 'active to' date for user at index {usernameIndex} to {newActiveTo.Date}", true);

            bool isDateChanged = false;

            IUser user = GetUserByIndex(usernameIndex);

            if (user != null)
            {
                user.ActiveTo = newActiveTo;
                isDateChanged = true;
            }

            WriteLine(isDateChanged ? $"Date has been updated for user {user.Username}." : $"User {user.Username} cannot be found!");
        }

        public void AssignUserRole(int usernameIndex, UserRoles newRole)
        {
            Logger.LogActivity($"Changing user role for user with index {usernameIndex} to {newRole}", true);

            bool isUserRoleChanged = false;

            User user = (User)GetUserByIndex(usernameIndex);

            if (user != null)
            {
                user.UserRole = newRole;
                isUserRoleChanged = true;
            }

            Logger.LogActivity(isUserRoleChanged ? $"Role of the {user.Username} has been updated." : "User not found!", true);
        }

        private IUser GetUserByIndex(int index)
        {
            IUser user = GetUsersData().ElementAtOrDefault(index);

            var logMsg = user != null ? $"Found user {user.Username} at index {index}." : $"No user found at index {index}!";

            Logger.LogActivity(logMsg, true);

            return user;
        }

        public Dictionary<string, int> ListAllUsers()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            for (int i = 0; i < GetUsersData().Count; i++)
            {
                result.Add(GetUsersData()[i].Username, i);
            }

            return result;
        }
    }
}
