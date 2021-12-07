using Microsoft.AspNetCore.SignalR;
using ProjetSessionAppWeb3.Models;
using ProjetSessionAppWeb3.Respositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetSessionAppWeb3.Models;
using ProjetSessionAppWeb3.Respositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ProjetSessionAppWeb3.Hubs
{
    /// <summary>
    /// Cette classe permet d'appeler des méthodes pour pouvoir envoyer des messages
    /// </summary>
    /// <author>A. Alperen, B. Shajaan et I. Gafran</author>
    public class ChatHub : Hub
    {
        /*  public void Send(string name, string message)
          {
              // Call the addNewMessageToPage method to update clients.
              Clients.All.addNewMessageToPage(name, message);

          }*/

        private IMessageRepository _mr;

        public ChatHub(IMessageRepository repository)
        {
            _mr = repository;
        }

        /// <summary>
        /// Cette méthode permet d'envoyer des messages d'un utilisateur à un autre
        /// </summary>
        /// <param name="user">Le nom de l'utilisateur qui envoie un message</param>
        /// <param name="message">Le message que l'utilisateur a envoyé</param>
        /// <returns>Les messages</returns>
        public async Task SendMessage(string user, string message)
        {
            
          //  Message msg = new Message();

         //   msg.IdChat = Int32.Parse(idChat);
         //   msg.IdUser = Int32.Parse(idUser);
          //  msg.Username = user;
        //    msg.Description = message;
          //  msg.DateMessaged = DateTime.Now;
            
           // await _mr.Create(msg);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>
        /// Cette méthode permet d'envoyer des messages de manières priver
        /// </summary>
        /// <param name="user">Le nom de l'utilisateur qui envoie un message</param>
        /// <param name="message">Le message que l'utilisateur a envoyé</param>
        /// <returns>Les messages</returns>
        public Task SendPrivateMessage(string user, string message)
        {
            return Clients.User(user).SendAsync("ReceiveMessage", message);
        }

        /// <summary>
        /// Cette méthode permet d'envoyer des messages dans des groupes
        /// </summary>
        /// <param name="groupName">Nom du groupe</param>
        /// <param name="message">Le message a envoyé</param>
        /// <returns>Les messages</returns>
        public Task SendMessageToGroup(string groupName, string message)
        {
            return Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId}: {message}");
        }

        /// <summary>
        /// Cette méthode permet d'ajouter des utilisateurs à des groupes
        /// </summary>
        /// <param name="groupName">Nom du groupe</param>
        /// <returns>Les messages</returns>
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        /// <summary>
        /// Cette méthode permet denlever des utilisateurs des groupes
        /// </summary>
        /// <param name="groupName">Nom du groupe</param>
        /// <returns>Les messages</returns>
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}
