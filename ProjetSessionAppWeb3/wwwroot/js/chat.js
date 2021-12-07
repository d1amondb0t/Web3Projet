"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    //var li = document.createElement("li");
   // document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    //li.textContent = `${user} says ${message}`;
    
    var userSent = user;
    var messageSent = message;
    sentMsg(userSent,messageSent);
    


});

function sentMsg(user, message) {

    var tbody = document.getElementById("test");
    var username = user;

    var today = new Date();
    var timeDay = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    var dateTime = timeDay + ' ' + time

    const div1 = document.createElement("div");
    div1.classList.add("row", "mb-2", "p-2");

    var userName = document.getElementById("userInput").value;
    
    

    //if (username != username) {
    //    console.log("HERE");
    //    div2 = div2.classList.remove("bg-primary");
    //    div2 = div2.classList.add("bg-info", "offset-6");
    //}

    const div2 = document.createElement("div");
    div2.classList.add("col-12", "text-white", "bg-primary", "rounded");


    const div3 = document.createElement("div");
    div3.classList.add("row", "border-bottom", "border-white");

    const div4 = document.createElement("div");
    div4.classList.add("col-6", "text-left");

    const p5 = document.createElement("p");
    p5.classList.add("text-left", "font-weight-bold");

    const div6 = document.createElement("div");
    div6.classList.add("col-6", "text-right");

    const p7 = document.createElement("p");

    const p8 = document.createElement("p");

    const contextPUsername = document.createTextNode(user);
    const contextDate = document.createTextNode(dateTime);
    const contextDescription = document.createTextNode(message);

    p7.appendChild(contextDate);
    p8.appendChild(contextDescription);
    p5.appendChild(contextPUsername);

    div1.appendChild(div2);
    div2.appendChild(div3);
    div2.appendChild(p8);
    div3.appendChild(div4);
    div4.appendChild(p5);
    div3.appendChild(div6)
    div6.appendChild(p7);
    
    tbody.appendChild(div1);



}

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    //var idChat = document.getElementById("chatId").value;
   // var idUser = document.getElementById("userId").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    
    const xhr = new XMLHttpRequest();
    var user = document.getElementById("userInput").value;
    var idChat = document.getElementById("chatId").value;
    var idUser = document.getElementById("userId").value;
    var message = document.getElementById("messageInput").value;
    console.log(idUser);
    console.log(idChat);
    const url = "https://localhost:44384/Room/SendMessage?user=" + user + "&message=" + message + "&idChat=" + idChat + "&idUser="+idUser;
    xhr.open("POST", url, true);
    xhr.send(user,message,idChat,idUser);
    
});
