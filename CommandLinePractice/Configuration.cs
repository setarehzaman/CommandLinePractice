
namespace CommandLinePractice
{
    public static class Configuration
    {
        public static string ConnectionString { get; set; }

        static Configuration()
        {
            ConnectionString =
                @"Data Source=SETAREH\SQLEXPRESS;Initial Catalog=MaktabUsers;User Id=sa;Password=سثظش1020; TrustServerCertificate=True";
        }
    }
}
