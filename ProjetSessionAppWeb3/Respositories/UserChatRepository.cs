using ProjetSessionAppWeb3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetSessionAppWeb3.Respositories
{
    /// <summary>
    /// Cette classe permet de manipuler les chats des utilisateurs
    /// </summary>
    /// <author>A. Alperen, B. Shajaan et I. Gafran</author>
    public class UserChatRepository : IUserChatRepository
    {
        private readonly DataBaseContext _context;

        public UserChatRepository(DataBaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creer un chat
        /// </summary>
        /// <param name="uc">Le chat utilisateur</param>
        /// <returns>Le chat utilisateur</returns>
        public async Task CreateUserChat(UserChat uc)
        {
            _context.UserChats.Add(uc);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Supprimer le chat de l'utilisateur
        /// </summary>
        /// <param name="uc">Le chat utilisateur</param>
        /// <returns>Le chat utilisateur</returns>
        public async Task DeleteUserChat(UserChat uc)
        {
            _context.UserChats.Remove(uc);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Trouver tous les chats d'un utilisateur spécifique
        /// </summary>
        /// <param name="userId">ID de l'tulisateur</param>
        /// <returns>Tous les chats de cet utilisateur</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<IEnumerable<UserChat>> GetAllChatsWhereParticipating(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
