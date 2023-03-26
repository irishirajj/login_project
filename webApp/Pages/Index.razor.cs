using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using webApp.Data;

namespace webApp.Pages
{
    public partial class Index
    {
        [Inject]
        public IDatabaseConnection DatabaseConnection { get; set; }
        public Users User { get; set; } = new();
        public IEnumerable<Users>? UsersList { get; set; }
        public string? Error { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        public async Task GetUsers()
        {
            UsersList = await Users.GetallUsers(DatabaseConnection);
        }
        protected override async Task OnInitializedAsync()
        {
            await GetUsers();
        }
        public async Task Login()
        {
            
            if (User.UserName == null)
            {
                Error = "Enter UserName";
                return;
            }
            else
            {

                Error =string.Empty;
            }
            if(User.Password == null)
            {
                Error = "Enter Password";
                return;
            }
            else
            {
                Error = string.Empty;
            }
            Users user = UsersList.FirstOrDefault(x => x.UserName == User.UserName);
            if (user==null)
            {
                Error = "User Does Not Exist";
                return;
            }
            else
            {
                if(UsersList.FirstOrDefault(x=>x.UserName == User.UserName).Password != User.Password)
                {
                    Error = "Wrong Password";
                    return;
                }
                else
                {
                    Error = string.Empty;
                    navigationManager.NavigateTo("/UserDetails");
                }
                Error =string.Empty;
          
            }
        }
    }
}
