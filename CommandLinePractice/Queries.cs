
namespace CommandLinePractice
{
    public class Queries
    {
        public static string Create = "INSERT INTO Users (Username, Password, IsAvailable) VALUES (@Username, @Password, @IsAvailable)";
        public static string Update = "UPDATE Users SET Password = @Password, IsAvailable = @IsAvailable WHERE Id = @Id";
        public static string PrefixUserName = "SELECT * FROM Users WHERE Username LIKE @UsernamePrefix";
        public static string SearchByUserName = "SELECT * FROM Users WHERE UserName = @UserName";

    }
}