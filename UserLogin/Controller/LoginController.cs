using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLogin.Model;
using UserLogin.View;

namespace UserLogin.Controller
{
    class UserLoginController
    {
        private readonly IUserData _userData;
        private IUser _currentUser;
        private readonly IView _currentView;

        public UserLoginController()
        {
            _currentView = new LoginView();
            _userData = new UserData();
            _currentUser = new User();
        }

        public void Run()
        {
            MainLoop();
        }

        private void HandleLogin()
        {
            

            Console.WriteLine("Username: ");
            string username = Console.ReadLine();

            Console.WriteLine("Password: ");
            string password = Console.ReadLine();

            LoginValidation loginValidation = new LoginValidation(username, password, Console.WriteLine);
            if (!loginValidation.ValidateUserInput(ref _currentUser, _userData))
            {
                Console.WriteLine("\nLogin failed!");
                Console.ReadLine();
                return;
            }

            string userRoleMessage = "\n{0} -> {1} has logged in.\n";
            switch (_currentUser.UserRole)
            {
                case UserRoles.ADMIN:
                    {
                        userRoleMessage = string.Format(userRoleMessage, UserRoles.ADMIN, _currentUser.Username);
                        break;
                    }
                case UserRoles.STUDENT:
                    {
                        userRoleMessage = string.Format(userRoleMessage, UserRoles.STUDENT, _currentUser.Username);
                        break;
                    }
                case UserRoles.PROFESSOR:
                    {
                        userRoleMessage = string.Format(userRoleMessage, UserRoles.PROFESSOR, _currentUser.Username);
                        break;
                    }
                case UserRoles.INSPECTOR:
                    {
                        userRoleMessage = string.Format(userRoleMessage, UserRoles.INSPECTOR, _currentUser.Username);
                        break;
                    }                
                default:
                    {
                        Console.Write($"Something went wrong. This role is not defined: {_currentUser.UserRole}!\n");
                        break;
                    }
                    
            }

            Console.WriteLine(userRoleMessage);
            Console.ReadLine();
        }

        private void MainLoop()
        {
            HandleLogin();

            if (_currentUser.UserRole == UserRoles.ADMIN)
            {
                while (true)
                {
                    _currentView.ShowMenu();
                    GoToAdminMode();
                }
            }
            else
            {
                Console.WriteLine("You don't have permissions to access admin data!");
            }
        }

        private void GoToAdminMode()
        {

            string input = Console.ReadLine();
            
            Dictionary<string, int> allUsers = _userData.ListAllUsers();

            if (allUsers == null)
            {
                Console.Write("No user data found!\n");
                return;
            }


            if(int.TryParse(input.ToString(), out int userInput))
            {
                switch (userInput)
                {
                    case 0:
                        {
                            Environment.Exit(0);
                            break;
                        }
                    case 1:
                        {
                            Console.WriteLine("Enter username of the user you want to edit: ");
                            string userToEdit = Console.ReadLine();

                            if (!allUsers.ContainsKey(userToEdit))
                            {
                                Console.Write($"User not found: {userToEdit}!");
                                break;
                            }

                            Console.WriteLine("Enter new role: ");
                            string newRoleInput = Console.ReadLine();
                            if (!Enum.TryParse(newRoleInput, true, out UserRoles newRole))
                            {
                                Console.Write($"Invalid role: {newRoleInput}!\n");
                                break;
                            }

                            _userData.AssignUserRole(allUsers[userToEdit], newRole);
                        }
                        break;
                    case 2:
                        {
                            Console.WriteLine("Enter the username you want to edit: ");
                            string userToEdit = Console.ReadLine();
                            if (!allUsers.ContainsKey(userToEdit))
                            {
                                Console.Write($"User not found: {userToEdit}!\n");
                                break;
                            }

                            Console.WriteLine("Enter new date: ");
                            string newUserActiveToDateInput = Console.ReadLine();
                            if (!DateTime.TryParse(newUserActiveToDateInput, out DateTime newUserActiveToDate) || newUserActiveToDate < DateTime.Now)
                            {
                                Console.Write($"Invalid date: {newUserActiveToDateInput}!\n");
                                break;
                            }

                            _userData.SetUserActiveTo(allUsers[userToEdit], newUserActiveToDate);
                        }
                        break;
                    case 3:
                        {
                            foreach (IUser u in _userData.GetUsersData())
                            {
                                Console.WriteLine(u);
                            }
                        }
                        break;
                    case 4:
                        {
                            Console.Write("Enter a log filter: ");
                            string keyWord = Console.ReadLine();

                            foreach (string logEntry in Logger.GetCurrentSessionActivities(keyWord))
                            {
                                Console.Write(logEntry);
                            }
                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine(Logger.GetAllUserActivities());
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Invalid input: " + input + "!\n");
                        }
                        break;
                }
            }

            Console.Write("\nPress any key to continue...");

            Console.ReadKey();
            Console.Clear();
        }
    }
}
