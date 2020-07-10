using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin.Model
{
    public interface IUserData
    {
        void ResetTestUserData();
        IReadOnlyList<IUser> GetUsersData();
        IUser IsUserPassCorrect(string username, string password);
        Dictionary<string, int> ListAllUsers();
        void AssignUserRole(int usernameIndex, UserRoles newRole);
        void SetUserActiveTo(int usernameIndex, DateTime newUserActiveToDate);
    }
}
