namespace APIBIM.DTO
{
    public class UserDTO
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;
    }
}
