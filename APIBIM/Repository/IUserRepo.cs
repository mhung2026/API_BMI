using APIBIM.DTO;
using APIBIM.Models;

namespace APIBIM.Repository
{
    public interface IUserRepo
    {
        public User login(LoginDTO account);
    }
}
