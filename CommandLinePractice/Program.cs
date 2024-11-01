using Colors.Net;
using Colors.Net.StringColorExtensions;
using System.ComponentModel.Design;

UserService userService = new();
User currentUser = null;


ColoredConsole.WriteLine("Setareh's User Management System ".Magenta());

while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine().Split(' ');

    if (input != null && input.Length != 0)
    {
        var command = input[0].ToLower();

        if (command == "cls")
        {
            Console.Clear();
            continue;
        }
        if (command == "exit")
        {
            ColoredConsole.WriteLine("Exiting The System...".DarkYellow());
            Environment.Exit(0);
        }
        if (command == "register")
        {
            RegisterUser(input);
        }
        else if (command == "login")
        {
            LoginUser(input);
        }
        else if (command == "changestatus")
        {
            ChangeUserStatus(input);
        }
        else if (command == "changepassword")
        {
            ChangeUserPassword(input);
        }
        else if (command == "search")
        {
            SearchUsers(input);
        }
        else if (command == "logout")
        {
            LogoutUser();
        }
        else
        {
            ColoredConsole.WriteLine("Unknown command".Yellow());
        }
    }
}
string GetValue(string[] command, string option)
{
    for (int i = 0; i < command.Length - 1; i++)
    {
        if (command[i] == option)
            return command[i + 1];
    }
    return null;
}
void RegisterUser(string[] command)
{
    var username = GetValue(command, "--username");
    var password = GetValue(command, "--password");

    if (username != null && password != null)
    {
        ColoredConsole.WriteLine(userService.Register(username, password));
    }
    else
    {
        ColoredConsole.WriteLine("help: register --username <username> --password <password>".DarkYellow());
    }
}

void LoginUser(string[] command)
{
    var username = GetValue(command, "--username");
    var password = GetValue(command, "--password");

    if (username != null && password != null)
    {
        if (userService.Login(username, password, out var user))
        {
            currentUser = user;
            ColoredConsole.WriteLine("Login successful!".Green());
        }
        else
        {
            ColoredConsole.WriteLine("Login failed! Incorrect username or password.".Red());
        }
    }
    else
    {
        ColoredConsole.WriteLine("help: login --username <username> --password <password>".DarkYellow());
    }
}

void ChangeUserStatus(string[] command)
{
    if (currentUser == null)
    {
        ColoredConsole.WriteLine("Error: You must be logged in to change your status.".Red());
        return;
    }

    var status = GetValue(command, "--status");
    var afterNot = GetValue(command, "not");

    if (status != null)
    {
        if (status is "available")
        {
            userService.ChangeStatus(currentUser,true);
            ColoredConsole.WriteLine($"Status changed to available".Green());
        }
        else if(status is "not" && afterNot is "available")
        {
            userService.ChangeStatus(currentUser,false);
            ColoredConsole.WriteLine("Status changed to not available".Green());
        }
        else
        {
            ColoredConsole.WriteLine("Error: You Must choose between <available|not available>!".Red());
        }
    }
    else
    {
        ColoredConsole.WriteLine("help: changestatus --status <available|not available>".DarkYellow());
    }
}

void ChangeUserPassword(string[] command)
{
    if (currentUser == null)
    {
        ColoredConsole.WriteLine("Error: You must be logged in to change your password.".Red());
        return;
    }

    var oldPassword = GetValue(command, "--old");
    var newPassword = GetValue(command, "--new");

    if (oldPassword != null && newPassword != null)
    {
        if (userService.ChangePassword(currentUser, oldPassword, newPassword))
        {
            ColoredConsole.WriteLine("Password changed successfully!".Green());
        }
        else
        {
            ColoredConsole.WriteLine("Error: Old password is incorrect.".Red());
        }
    }
    else
    {
        ColoredConsole.WriteLine("help: changePassword --old <oldPassword> --new <newPassword>".DarkYellow());
    }
}

void SearchUsers(string[] command)
{
    if (currentUser == null)
    {
        ColoredConsole.WriteLine("Error: You must be logged in to search for users".Red());
        return;
    }

    var usernamePrefix = GetValue(command, "--username");
    if (usernamePrefix != null)
    {
        var users = userService.SearchUsers(usernamePrefix);
        foreach (var user in users)
        {
            Console.WriteLine($"{user.UserName} | status: {(user.IsAvailable ? "available" : "not available")}");
        }
    }
    else
    {
        ColoredConsole.WriteLine("help: search --username <prefix>".DarkYellow());
    }
}

void LogoutUser()
{
    if (currentUser != null)
    {
        currentUser = null;
        ColoredConsole.WriteLine("Logged out successfully!".Green());
    }
    else
    {
        ColoredConsole.WriteLine("No user is currently logged in.".Red());
    }
}


