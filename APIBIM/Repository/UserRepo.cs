using APIBIM.DTO;
using APIBIM.Models;

namespace APIBIM.Repository
{
    public class UserRepo :IUserRepo
    {
        private readonly SwtLabApiContext _SwtLabApiContext;
        public UserRepo(SwtLabApiContext swpRealEstateContext)
        {
            _SwtLabApiContext = swpRealEstateContext;
        }
        public User login(LoginDTO user)
        {
            User account = _SwtLabApiContext.Users.Where(x => x.Username.Equals(user.Username) && x.PasswordHash.Equals(user.PasswordHash)).FirstOrDefault();
            return account;
        }
    }
}
