using ProjetSessionAppWeb3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetSessionAppWeb3.Respositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessagesParChat(int chatId);
        Task Create(Message m);
        Task Delete(Message m);
        Task Update(Message m);
    }
}
