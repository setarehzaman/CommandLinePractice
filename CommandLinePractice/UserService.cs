
using Colors.Net.StringColorExtensions;
using CommandLinePractice;

public class UserService
{
    IUserRepository _userRepository;

    public UserService()
    {
        _userRepository = new UserDapperRepository();
    }

    public string Register(string username, string password)
    {
        if (_userRepository.GetUserByUsername(username) != null)
            return $"{"register failed! username already exists.".Red()}";

        var user = new User() { UserName = username, Password = password};

        _userRepository.AddUser(user);
        return $"{"Registration successful!".Green()}";
    }


    public bool Login(string username, string password, out User user)
    {
        user = _userRepository.GetUserByUsername(username);
        if (user != null)
        {
            if (user.Password == password)
            {
                return true;
            }
        }
        return false;
    }

    public void ChangeStatus(User user, bool isAvailable)
    {
        user.IsAvailable = isAvailable;
        _userRepository.UpdateUser(user);
    }

    public bool ChangePassword(User user, string oldPassword, string newPassword)
    {
        if (user.Password != oldPassword)
            return false;

        user.Password = newPassword;
        _userRepository.UpdateUser(user);
        return true;
    }

    public List<User> SearchUsers(string usernamePrefix)
    {
        return _userRepository.GetUsersByPrefix(usernamePrefix);
    }
}
