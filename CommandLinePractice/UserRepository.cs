
using Newtonsoft.Json;

public class UserRepository : IUserRepository
{
    const string _filePath = "D:/users.json";
    Dictionary<string, User> Users;

    public UserRepository()
    {
        LoadUsers();
    }
    private void LoadUsers()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);

            Users = JsonConvert.DeserializeObject<Dictionary<string, User>>(json);

            if (Users == null)
            {
                Users = new Dictionary<string, User>();
            }
        }
        else
        {
            Users = new Dictionary<string, User>();
        }
    }

    private void SaveUsers() => File.WriteAllText(_filePath, JsonConvert.SerializeObject(Users));

    public User GetUserByUsername(string username)
    {
        Users.TryGetValue(username, out var user);
        return user;
    }

    public void AddUser(User user)
    {
        if (!Users.ContainsKey(user.UserName))
        {
            Users[user.UserName] = user;
            SaveUsers();
        }
    }

    public void UpdateUser(User user)
    {
        if (Users.ContainsKey(user.UserName))
        {
            Users[user.UserName] = user;
            SaveUsers();
        }
    }

    public List<User> GetUsersByPrefix(string usernamePrefix)
    { 
        return Users.Values.Where(u => u.UserName.StartsWith(usernamePrefix)).ToList();

    }
}
