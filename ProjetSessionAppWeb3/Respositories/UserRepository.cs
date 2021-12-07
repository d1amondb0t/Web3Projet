using Microsoft.EntityFrameworkCore;
using ProjetSessionAppWeb3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetSessionAppWeb3.Respositories
{
    /// <summary>
    /// Cette classe permet de manipuler les informations de l'utilisateur
    /// </summary>
    /// <author>A. Alperen, B. Shajaan et I. Gafran</author>
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _context;
        private int id;

        public UserRepository(DataBaseContext context)
        {
            _context = context;
            if (context.Users.Count() > 0)
            {
                id = _context.Users.AsEnumerable().Last().IdUser;
                _context.ChangeTracker.Clear();
            }
            else
            {
                id = 0;
            }
        }

        /// <summary>
        /// Créer un utilisateur
        /// </summary>
        /// <param name="u">Lutilisateur</param>
        /// <returns>L'utilisateur</returns>
        public async Task Create(User u)
        {
            _context.Users.Add(u);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Trouver tous les utilisateurs par leur nom
        /// </summary>
        /// <param name="nom">Le nom de l'utilisateur recherché</param>
        /// <returns>L'utilisateur</returns>
        public async Task<IEnumerable<User>> GetAllUsersByNom(string nom)
        {
            return await _context.Users.Where(u => u.Username.StartsWith(nom)).ToListAsync();
        }

        /// <summary>
        /// Modifier l'utilisateur
        /// </summary>
        /// <param name="u">L'utilisateur</param>
        /// <returns>L'utilisateur</returns>
        public async Task Update(User u)
        {
            _context.Users.Update(u);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Méthode qui permet à l'utilisateur de login
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="password">Mot de passe de l'utilisateur</param>
        /// <returns>L'utilisateur</returns>
        public async Task<User> UserLogin(string nom, string password)
        {
            IQueryable<User> query = _context.Users.Where(u => u.Username.
            Equals(nom) &&
            u.Password.Equals(password));
            if (query.Any())
            {
                return await query.FirstOrDefaultAsync();
            }
            else
            {
                User nullUtilisateur = new User
                {
                    IdUser = 0
                };
                return nullUtilisateur;
            }
        }
    }
}
