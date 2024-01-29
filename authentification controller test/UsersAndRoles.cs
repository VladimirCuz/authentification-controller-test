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
        public virtual ICollection<Comment> Comments { get; set; }
    }
    public class Role 
    {
        public string name { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string permissions { get; set; } = string.Empty;
        public virtual ICollection<UserRole> UserRoles { get; set; } 
    }
    public class UserRole
    {
        public int UserId;
        public User User;
        public int RoleId;
        public Role Role;

    }
}
