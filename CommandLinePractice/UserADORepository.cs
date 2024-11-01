

using Microsoft.Data.SqlClient;
using System.Data;

namespace CommandLinePractice
{
    public class UserADORepository : IUserRepository
    {
        private User MapUser(SqlDataReader reader)
        {
            return new User
            {
                Id = (int)reader["Id"],
                UserName = (string)reader["UserName"],
                Password = (string)reader["Password"],
                IsAvailable = (bool)reader["IsAvailable"]
            };
        }
        public User GetUserByUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Queries.SearchByUserName, connection))
                {
                    command.Parameters.AddWithValue("UserName", username);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = (int)reader["Id"],
                                UserName = (string)reader["UserName"],
                                Password = (string)reader["Password"],
                                IsAvailable = (bool)reader["IsAvailable"]

                            };
                        }
                    }
                }
            }
            return null;
        }

        public void AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Queries.Create, connection))
                {
                    command.Parameters.AddWithValue("UserName", user.UserName);
                    command.Parameters.AddWithValue("Password", user.Password);
                    command.Parameters.AddWithValue("IsAvailable", user.IsAvailable);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Queries.Update, connection))
                {
                    command.Parameters.AddWithValue("Id", user.Id);
                    command.Parameters.AddWithValue("Password", user.Password);
                    command.Parameters.AddWithValue("IsAvailable", user.IsAvailable);



                    command.ExecuteNonQuery();
                }
            }
        }

        public List<User> GetUsersByPrefix(string usernamePrefix)
        {
            var users = new List<User>();
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Queries.PrefixUserName, connection))
                {
                    command.Parameters.AddWithValue("@UserNamePrefix", $"{usernamePrefix}%");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                Id = (int)reader["Id"],
                                UserName = (string)reader["UserName"],
                                Password = (string)reader["Password"],
                                IsAvailable = (bool)reader["IsAvailable"]
                            });
                        }
                    }
                }
            }
            return users;
        }
    }
}
