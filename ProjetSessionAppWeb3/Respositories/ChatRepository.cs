using Microsoft.EntityFrameworkCore;
using ProjetSessionAppWeb3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetSessionAppWeb3.Respositories
{
    /// <summary>
    /// Cette classe permet de manipuler les chats (Creer un chat ou supprimer un chat)
    /// </summary>
    /// <author>A. Alperen, B. Shajaan et I. Gafran</author>
    public class ChatRepository : IChatRepository
    {
        private readonly DataBaseContext _context;
        private int id;

        public ChatRepository(DataBaseContext context)
        {
            _context = context;
            if (_context.Chats.Count() > 0)
            {
                id = _context.Chats.AsEnumerable().Last().IdChat;
                _context.ChangeTracker.Clear();
            }
            else
            {
                id = 0;
            }
        }

        /// <summary>
        /// Créer un chat
        /// </summary>
        /// <param name="c">Nom du chat</param>
        /// <returns>Création du chat</returns>
        public async Task CreateChat(Chat c)
        {
            _context.Chats.Add(c);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Supprimer un chat
        /// </summary>
        /// <param name="id">Nom du chat</param>
        /// <returns>Supression du chat</returns>
        public async Task DeleteChat(int id)
        {
            var deleteChat = _context.Chats.Find(id);
            _context.Chats.Remove(deleteChat);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Trouver tous les chats
        /// </summary>
        /// <returns>Tous les chats</returns>
        public async Task<IEnumerable<Chat>> GetAllChats()
        {
            return await _context.Chats.ToListAsync();
        }

        /// <summary>
        /// Trouver tous les chats par leur nom
        /// </summary>
        /// <param name="name">Nom du chat</param>
        /// <returns>Le chat rechercher</returns>
        public async Task<IEnumerable<Chat>> GetAllChatsByName(string name)
        {
            return await _context.Chats.Where(c => c.ChatName.StartsWith(name)).ToListAsync();
        }

        /// <summary>
        /// Trouver un chat avec le ID du créateur du chat
        /// </summary>
        /// <param name="id">ID du créateur</param>
        /// <returns>Le chat recherche</returns>
        public async Task<IEnumerable<Chat>> GetAllChatsWhereCreator(int id)
        {
            return await _context.Chats.Where(c => c.IdCreator.Equals(id)).ToListAsync();
        }
    }
}
