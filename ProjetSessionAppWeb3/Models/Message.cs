using System;


namespace ProjetSessionAppWeb3.Models
{
    /// <summary>
    /// Cette classe représente les informations reliées aux messages
    /// </summary>
    /// <author>A. Alperen, B. Shajaan et I. Gafran</author>
    public partial class Message
    {
        /// <summary>
        /// ID du message envoyé
        /// </summary>
        public int IdMessage { get; set; }

        /// <summary>
        /// ID du chat
        /// </summary>
        public int IdChat { get; set; }

        /// <summary>
        /// ID de l'utilisateur
        /// </summary>
        public int IdUser { get; set; }

        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Date du message envoyé
        /// </summary>
        public DateTime DateMessaged { get; set; }

        /// <summary>
        /// Le message que l'utilisateur a envoyé
        /// </summary>
        
        public string Description { get; set; }
        public virtual Chat IdChatReference { get; set; }
        public virtual User IdUserReference { get; set; }
    }
}
