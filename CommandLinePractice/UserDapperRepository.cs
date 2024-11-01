

using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CommandLinePractice
{
    public class UserDapperRepository : IUserRepository
    {
        Dictionary<string, User> Users;
        public void AddUser(User user)
        {
            using (IDbConnection db = new SqlConnection(Configuration.ConnectionString))
            {
                db.Execute(Queries.Create, new { user.UserName, user.Password, user.IsAvailable});
            }
        }
        public User GetUserByUsername(string username)
        {
            using (IDbConnection db = new SqlConnection(Configuration.ConnectionString))
            {
               
                var user = db.QueryFirstOrDefault<User>(Queries.SearchByUserName, new { UserName = username });
                return user;
            }
        }
        public List<User> GetUsersByPrefix(string usernamePrefix)
        {
            using (IDbConnection db = new SqlConnection(Configuration.ConnectionString))
            {
                return db.Query<User>(Queries.PrefixUserName, new { UsernamePrefix = $"{usernamePrefix}%" }).ToList();
            }
        }
        public void UpdateUser(User user)
        {
            using (IDbConnection db = new SqlConnection(Configuration.ConnectionString))
            {
                db.Execute(Queries.Update, new { user.Password, user.IsAvailable, user.Id });
            }
        }
    }
}
