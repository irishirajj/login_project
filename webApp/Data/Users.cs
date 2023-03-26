using Dapper;
using Dapper.Contrib.Extensions;

namespace webApp.Data
{
    [Table("Users")]
    public class Users
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? EmailId { get; set; }
        public string? ManagerName { get; set; }
        public DateTime? AccCreationDate { get; set; }


        public async static Task<IEnumerable<Users>> GetallUsers(IDatabaseConnection databaseConnection)
        {
            using var connection = databaseConnection.GetConnection();
            return await connection.QueryAsync<Users>(@"Select * from Users;");
        }
        
    }
}
