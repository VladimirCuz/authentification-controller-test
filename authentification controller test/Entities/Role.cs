namespace authentification_controller_test.Entities
{
    public class Role
    {
        public string name { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string permissions { get; set; } = string.Empty;
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
