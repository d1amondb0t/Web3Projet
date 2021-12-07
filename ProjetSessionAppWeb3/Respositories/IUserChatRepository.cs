using ProjetSessionAppWeb3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetSessionAppWeb3.Respositories
{
    public interface IUserChatRepository
    {
        Task CreateUserChat(UserChat uc);
        Task DeleteUserChat(UserChat uc);

        Task<IEnumerable<UserChat>> GetAllChatsWhereParticipating(int userId);
    }
}
