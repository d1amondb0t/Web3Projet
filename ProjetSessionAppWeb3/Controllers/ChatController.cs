using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetSessionAppWeb3.Models;
using ProjetSessionAppWeb3.Respositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetSessionAppWeb3.Controllers
{
    /// <summary>
    /// Le controlleur Chat qui permet de relier le modèle de Chat avec les vues des Chats et aussi de faire des manipulations sur les chats
    /// </summary>
    /// <author>A. Alperen, B. Shajaan et I. Gafran</author>
    public class ChatController : Controller
    {
        private readonly IChatRepository _cr;
        private readonly IUserChatRepository _ucr;
        private IMessageRepository _mr;
        
        public ChatController(IChatRepository cr, IUserChatRepository ucr,IMessageRepository repository)
        {
            _cr = cr;
            _ucr = ucr;
            _mr = repository;
        }
        /// <summary>
        /// Cette méthode permet d'envoyer des messages 
        /// </summary>
        /// <param name="user">Nom de l'utilisateur</param>
        /// <param name="message">Message envoyé</param>
        /// <param name="idChat">ID du chat</param>
        /// <param name="idUser">ID de l'utilisateur</param>
        [HttpPost("Room/SendMessage")]
        public async void SendMessage(string user, string message, string idChat, string idUser)
        {
            Message msg = new Message();

            msg.IdChat = Int32.Parse(idChat);
            msg.IdUser = Int32.Parse(idUser);
            msg.Username = user;
            msg.Description = message;
            msg.DateMessaged = DateTime.Now;

            await _mr.Create(msg);
            
        }
        
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //Get Session Info
            // var user = HttpContext.Session.GetInt32("userSession");
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userSession"));
            List<Chat> chats = (List<Chat>) await _cr.GetAllChats();
            ViewData["chats"] = chats;
            ViewBag.userSession = user;
            return View(user);
        }

        //[HttpPost]
        //public async void Chat(string chatName)
        //{
        //    Console.WriteLine(chatName);
        //    if (!chatName.Equals("") || chatName is not null)
        //    {
        //        var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userSession"));
        //        int id = user.IdUser;
        //        Chat newChat = new Chat();
        //        newChat.ChatName = chatName;
        //        newChat.IdCreator = id;
        //        await _cr.CreateChat(newChat);
        //    }
        //}

        /// <summary>
        /// Cette méthode permet d'ajouter des chats
        /// </summary>
        /// <param name="chatName">Nom du chat</param>
        /// <returns>Le chat crée</returns>
        [HttpPost]
        public async Task<ActionResult> AddChat(string chatName)
        {
            ViewData["errorMessageCreateChat"] = "";
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userSession"));
            Console.WriteLine(chatName);
            if (!chatName.Equals(""))
            {
                user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userSession"));
                int id = user.IdUser;
                Chat newChat = new Chat();
                newChat.ChatName = chatName;
                newChat.IdCreator = id;
                await _cr.CreateChat(newChat);
            }
            else
            {
                ViewData["errorMessageCreateChat"] = "Fill in the Chat Name";
            }

            List<Chat> chats = (List<Chat>)await _cr.GetAllChats();
            ViewData["chats"] = chats;
            ViewBag.userSession = user;

            user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userSession"));
            return View("Index");
        }

        /// <summary>
        /// Cette méthode permet de supprimer des chats
        /// </summary>
        /// <param name="idChat">ID du chat à supprimer</param>
        /// <returns>La suppression du chat</returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteChat(string idChat)
        {
            int id = Int16.Parse(idChat);
            await _cr.DeleteChat(id);
            List<Chat> chats = (List<Chat>)await _cr.GetAllChats();
            ViewData["chats"] = chats;
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userSession"));
            ViewBag.userSession = user;
            return View("Index");
        }

        /// <summary>
        /// Cette méthode permet d'ajouter des utilisateurs à des chats
        /// </summary>
        /// <param name="idChat">ID du chat</param>
        /// <param name="idUser">ID de l'utilisateur</param>
        /// <returns>L'ajout des utilisateurs</returns>
        [HttpPost]
        public async Task<ActionResult> AddUserToChat(string idChat,string idUser)
        {
            int idC = Int16.Parse(idChat);
            int idU = Int16.Parse(idUser);
            UserChat uc = new UserChat();
            uc.IdChat = idC;
            uc.IdUser = idU;
            await _ucr.CreateUserChat(uc);
            List<Chat> chats = (List<Chat>)await _cr.GetAllChats();
            ViewData["chats"] = chats;
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userSession"));
            ViewBag.userSession = user;
            return View("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Room(int id)
        {
            //Get Session Info
            // var user = HttpContext.Session.GetInt32("userSession");

            
            List<Message> msgs = (List<Message>)await _mr.GetMessagesParChat(id);
            if (msgs!=null)
            {
                ViewBag.chatMessages = msgs;
            }
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userSession"));
            List<Chat> chats = (List<Chat>)await _cr.GetAllChats();
            ViewData["chats"] = chats;
            ViewBag.userSession = user;
            ViewBag.idChat = id;
            return View();
        }

    }
}
