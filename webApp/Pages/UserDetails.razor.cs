using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Components;
using webApp.Data;

namespace webApp.Pages
{
    public partial class UserDetails
    {
        [Inject]
        public IDatabaseConnection DatabaseConnection { get; set; }
        public Users User { get; set; } = new();
        public IEnumerable<Users>? UsersList { get; set; }
        public string? Error { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        private bool showaddmodal = false;
        public async Task GetUsers()
        {
            UsersList = await Users.GetallUsers(DatabaseConnection);
        }
        protected override async Task OnInitializedAsync()
        {
            await GetUsers();
        }
        public void Showadd()
        {
            showaddmodal = true;
            Error = string.Empty;
        }
        public void hideadd()
        {
            showaddmodal = false;
            Error = string.Empty;
        }
        public async Task AddUser()
        {
            using var conn = DatabaseConnection.GetConnection();
            if (User != null)
            {
                try
                {
                    User.AccCreationDate = DateTime.Now;
                    await conn.InsertAsync(User);
                    hideadd();
                    await GetUsers();
                }
                catch(Exception ex)
                {
                    Error =ex.Message;
                    return;
                }
            }
        }
    }
}
