using System.Collections.Generic;


namespace ProjetSessionAppWeb3.Models
{
    /// <summary>
    /// Cette classe représente les informations reliées au chat
    /// </summary>
    /// <author>A. Alperen, B. Shajaan et I. Gafran</author>
    public partial class Chat
    {
        /// <summary>
        /// ID du chat
        /// </summary>
        public int IdChat { get; set; }
        
        /// <summary>
        /// Nom du chat
        /// </summary>
        public string ChatName { get; set; }
        
        /// <summary>
        /// ID du créateur du chat
        /// </summary>
        public int IdCreator { get; set; }

        public virtual User IdCreatorReference { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<UserChat> UserChats { get; set; }
    }
}
