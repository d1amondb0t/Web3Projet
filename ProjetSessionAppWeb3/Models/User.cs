using System.Collections.Generic;


namespace ProjetSessionAppWeb3.Models
{
    /// <summary>
    /// Cette classe représente les informations reliées à l'utilisateur
    /// </summary>
    /// <author>A. Alperen, B. Shajaan et I. Gafran</author>
    public partial class User
    {
        /// <summary>
        /// ID de l'utilisateur
        /// </summary>
        public int IdUser { get; set; }

        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Email de l'utilisateur
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Mot de passe de l'utilisateur
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Mot de passe de l'utilisateur
        /// </summary>
        public string Password2 { get; set; }

        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UserChat> UserChats { get; set; }
    }
}
