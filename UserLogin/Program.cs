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
