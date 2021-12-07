using ProjetSessionAppWeb3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetSessionAppWeb3.Respositories
{
    public interface IChatRepository
    {
        Task<IEnumerable<Chat>> GetAllChats();
        Task<IEnumerable<Chat>> GetAllChatsByName(string name);
        Task<IEnumerable<Chat>> GetAllChatsWhereCreator(int id);
        Task CreateChat(Chat c);
        Task DeleteChat(int id);
    }
}
