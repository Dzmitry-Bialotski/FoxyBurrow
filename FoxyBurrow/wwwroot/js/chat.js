const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

//Disable send button until connection is established
document.getElementById("sendMessageButton").disabled = true;

//write mess
hubConnection.on("ReceiveMessage", function (user_full_name, imagePath, messageText, messageDate, isWriter) {
    try {
        var fname = user_full_name;
        var ipath = imagePath;
        var mtext = messageText;
        var mdate = messageDatel;
        var isWr = isWriter; 
        //!TODO rewrite message
        /*var message = document.createElement("div");
        if (isWriter) {
            message.className = "message border border-light rounded bg-dark float-right";
            message.style = "margin: 8px; margin-left: 40px; width: 80% ";
        }
        else {
            message.className = "message border border-light rounded bg-dark float-left";
            message.style = "margin: 8px; margin-right: 40px; width: 80% ";
        }

        var message_profile = document.createElement("div");
        message_profile.className = "message-profile row";

        var h6 = document.createElement("h5");
        h6.className = "text-light";
        h6.style = "margin-left: 30px;";
        h6.textContent = profile_full_name;

        message_profile.appendChild(h6);
        message.appendChild(message_profile);

        var messText = document.createElement("h6");
        messText.className = "text-light";
        messText.textContent = messageText;
        var messDate = document.createElement("h6");
        messDate.className = "text-light";
        messDate.textContent = messageDate;

        message.appendChild(messText);
        message.appendChild(messDate);

        document.getElementById("messagesList").appendChild(message);
        */
    }
    catch (err) {
        alert(err);
    }

});
//start connection
hubConnection.start().then(function () {
    document.getElementById("sendMessageButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//trigger chatHub function
document.getElementById("sendMessageButton").addEventListener("click", function (event) {
    var userId = document.getElementById("user-id").value;
    var messageText = document.getElementById("messageText").value;
    var chatId = document.getElementById("chat-id").value;
    connection.invoke("SendMessage", chatId, userId, messageText).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("messageText").value = "";
    event.preventDefault();
});
