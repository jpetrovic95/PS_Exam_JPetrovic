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
            _currentView.PrintMessage("Username: ");
            string username = Console.ReadLine();

            _currentView.PrintMessage("Password: ");
            string password = Console.ReadLine();

            LoginValidation loginValidation = new LoginValidation(username, password, _currentView.PrintError);
            if (!loginValidation.ValidateUserInput(ref _currentUser, _userData))
            {
                _currentView.PrintError("\nLogin failed!");
                Console.ReadLine();
                return;
            }

            string userRoleMessage = "\n{0}: User {1} has logged in.\n";
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
                        _currentView.PrintError($"Something went wrong. This role is not defined: {_currentUser.UserRole}!\n");
                        break;
                    }
                    
            }

            _currentView.PrintMessage(userRoleMessage);
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
                _currentView.PrintMessage("You don't have permissions to access admin data!");
            }
        }

        private void GoToAdminMode()
        {

            string input = Console.ReadLine();
            Dictionary<string, int> listOfUsers = _userData.ListAllUsers();

            if (listOfUsers == null)
            {
                _currentView.PrintMessage("No user data found!\n");
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
                            _currentView.PrintMessage("Enter username of the user you want to edit: ");
                            string userToEdit = Console.ReadLine();

                            if (!listOfUsers.ContainsKey(userToEdit))
                            {
                                _currentView.PrintMessage($"User not found: {userToEdit}!");
                                break;
                            }

                            _currentView.PrintMessage("Enter new role: ");
                            string newRoleInput = Console.ReadLine();
                            if (!Enum.TryParse(newRoleInput, true, out UserRoles newUserRole))
                            {
                                _currentView.PrintError($"Invalid role: {newRoleInput}!\n");
                                break;
                            }
                            else
                            {
                                _currentView.PrintMessage("Role was succesfully updated.");
                            }
                            _userData.AssignUserRole(listOfUsers[userToEdit], newUserRole);
                        }
                        break;
                    case 2:
                        {
                            _currentView.PrintMessage("Enter the username you want to edit: ");
                            string userToEdit = Console.ReadLine();
                            if (!listOfUsers.ContainsKey(userToEdit))
                            {
                                _currentView.PrintMessage($"There is no such user!\n");
                                break;
                            }

                            _currentView.PrintMessage("Enter new date: ");
                            string newDate = Console.ReadLine();
                            if (!DateTime.TryParse(newDate, out DateTime newUserDate) || newUserDate < DateTime.Now)
                            {
                                _currentView.PrintError($"Invalid date!\n");
                                break;
                            }

                            _userData.SetUserActiveTo(listOfUsers[userToEdit], newUserDate);
                        }
                        break;
                    case 3:
                        {
                            foreach (IUser u in _userData.GetUsersData())
                            {
                                _currentView.PrintMessage(u.ToString());
                            }
                        }
                        break;
                    case 4:
                        {
                            _currentView.PrintMessage("Enter a key word to search for: ");
                            string keyWord = Console.ReadLine();

                            foreach (string logEntry in Logger.GetCurrentSessionActivities(keyWord))
                            {
                                _currentView.PrintMessage(logEntry);
                            }
                        }
                        break;
                    case 5:
                        {
                            _currentView.PrintMessage(Logger.GetAllUserActivities());
                        }
                        break;
                    default:
                        {
                            _currentView.PrintError("Invalid input: " + input + "!\n");
                        }
                        break;
                }
            }

            _currentView.PrintMessage("\nPress any key to continue...");

            Console.ReadKey();
            Console.Clear();
        }
    }
}
