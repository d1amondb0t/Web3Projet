using Microsoft.EntityFrameworkCore;
using ProjetSessionAppWeb3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetSessionAppWeb3.Respositories
{
    /// <summary>
    /// Cette classe permet de manipuler les messages envoyés
    /// </summary>
    /// <author>A. Alperen, B. Shajaan et I. Gafran</author>
    public class MessageRepository : IMessageRepository
    {
        private readonly DataBaseContext _context;
        private int id;

        public MessageRepository(DataBaseContext context)
        {
            _context = context;
            if (_context.Messages.Count() > 0)
            {
                id = _context.Messages.AsEnumerable().Last().IdMessage;
                _context.ChangeTracker.Clear();
            }
            else
            {
                id = 0;
            }
        }
        /// <summary>
        /// Création du message
        /// </summary>
        /// <param name="m">Le message</param>
        /// <returns>Le message</returns>
        public async Task Create(Message m)
        {
            m.IdMessage = id + 1;
            _context.Messages.Add(m);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Supprimer un message
        /// </summary>
        /// <param name="m">Le message</param>
        /// <returns>Le message</returns>
        public async Task Delete(Message m)
        {
            _context.Messages.Remove(m);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Trouver tous les messages par chat
        /// </summary>
        /// <param name="chatId">ID du chat</param>
        /// <returns>Tous les chats</returns>
        public async Task<IEnumerable<Message>> GetMessagesParChat(int chatId)
        {
            return await _context.Messages.Where(m => m.IdChat == chatId).OrderBy(m => m.DateMessaged).ToListAsync();
        }

        /// <summary>
        /// Modifier un message
        /// </summary>
        /// <param name="m">Le message</param>
        /// <returns>Le message</returns>
        public async Task Update(Message m)
        {
            _context.Messages.Update(m);
            await _context.SaveChangesAsync();
        }
    }
}
