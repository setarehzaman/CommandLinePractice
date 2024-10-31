public class User
{
    public int Id { get; set; } // for future sql
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAvailable { get; set; }

    public User(string username, string passwordHash)
    {
        Username = username;
        Password = passwordHash;
        IsAvailable = true;
    }
}