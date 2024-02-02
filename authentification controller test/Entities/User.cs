using authentification_controller_test.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;

namespace authentification_controller_test
{
    public class User
    {
        public int UserId {  get; set; }
        public string login { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
