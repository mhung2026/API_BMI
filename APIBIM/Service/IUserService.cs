using APIBIM.DTO;
using APIBIM.Models;
using System.Linq.Expressions;

namespace APIBIM.Service
{
    public interface IUserService
    {
        public User createUser(UserDTO dto);
        public String login(LoginDTO dto);
    }
}
