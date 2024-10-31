public interface IUserRepository
{
    User GetUserByUsername(string username);
    void AddUser(User user);
    void UpdateUser(User user);
    List<User> GetUsersByPrefix(string usernamePrefix);
}
