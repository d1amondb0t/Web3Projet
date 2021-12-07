using ProjetSessionAppWeb3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetSessionAppWeb3.Respositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersByNom(string nom);
        Task<User> UserLogin(string nom, string password);
        Task Create(User u);
        Task Update(User u);
    }
}
