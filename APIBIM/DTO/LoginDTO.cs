namespace APIBIM.DTO
{
    public class LoginDTO
    {
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
