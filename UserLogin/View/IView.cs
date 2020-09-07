using UserLogin.Model;

namespace UserLogin.View
{
    public interface IView
    {
        void ShowMenu();
		void PrintMessage(string msg);
		void PrintError(string errorMsg);
        void PrintAllUsers(IUserData userData);
    }
}
