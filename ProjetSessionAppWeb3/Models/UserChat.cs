namespace ProjetSessionAppWeb3.Models
{
    /// <summary>
    /// Informations sur le chat
    /// </summary>
    /// <author>A. Alperen, B. Shajaan et I. Gafran</author>
    public partial class UserChat
    {
        /// <summary>
        /// ID de l'utilisateur
        /// </summary>
        public int IdUser { get; set; }
        
        /// <summary>
        /// ID du chat
        /// </summary>
        public int IdChat { get; set; }

        public virtual User IdUserReference { get; set; }
        public virtual Chat IdChatReference { get; set; }
    }
}
