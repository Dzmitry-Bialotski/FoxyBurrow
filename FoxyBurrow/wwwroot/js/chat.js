const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

//Disable send button until connection is established
document.getElementById("sendMessageButton").disabled = true;

//write mess
hubConnection.on("ReceiveMessage", function (imagePath, messageText, messageDate, isWriter) {
    try {
        if (isWriter) {
            /*
                                <div class="outgoing-chats">
                                    <div class="outgoing-chats-img">
                                        <img src="@Url.Content(imagePath)" />
                                    </div>
                                    <div class="outgoing-chats-msg">
                                        <p> @message.Text</p>
                                        <span class="time"> @message.MessageDate</span>
                                    </div>
                                </div>
             */
            var mess = document.createElement("div");
            mess.className = "outgoing-chats";

            var mess_img_block = document.createElement("div");
            mess_img_block.className = "outgoing-chats-img";
            var mess_img = document.createElement("img");
            mess_img.src = imagePath;

            var mess_text_block = document.createElement("div");
            mess_text_block.className = "outgoing-chats-msg";
            var p = document.createElement("p");
            p.textContent = messageText;
            var span = document.createElement("span");
            span.className = "time";
            span.textContent = messageDate;

            mess.appendChild(mess_img_block);
                mess_img_block.appendChild(mess_img);
            mess.appendChild(mess_text_block);
                mess_text_block.appendChild(p);
                mess_text_block.appendChild(span);
            document.getElementById("messagesList").appendChild(mess);
        }   
        else {
            /*
                                <div class="received-chats">
                                    <div class="received-chats-img">
                                        <img src="@Url.Content(imagePath)" />
                                    </div>
                                    <div class="received-msg">
                                        <div class="received-msg-inbox">
                                            <p> @message.Text</p>
                                            <span class="time"> @message.MessageDate</span>
                                        </div>
                                    </div>
                                </div>
             */
            var mess = document.createElement("div");
            mess.className = "received-chats";

            var mess_img_block = document.createElement("div");
            mess_img_block.className = "received-chats-img";
            var mess_img = document.createElement("img");
            mess_img.src = imagePath;

            var mess_text_block = document.createElement("div");
            mess_text_block.className = "received-msg";
            var mess_text_block_inbox = document.createElement("div");
            mess_text_block_inbox.className = "received-msg-inbox";
            var p = document.createElement("p");
            p.textContent = messageText;
            var span = document.createElement("span");
            span.className = "time";
            span.textContent = messageDate;

            mess.appendChild(mess_img_block);
            mess_img_block.appendChild(mess_img);
            mess.appendChild(mess_text_block);
            mess_text_block.appendChild(mess_text_block_inbox);
            mess_text_block_inbox.appendChild(p);
            mess_text_block_inbox.appendChild(span);
            document.getElementById("messagesList").appendChild(mess);
        }
        var scroll = document.getElementById('scroll_chat');
        scroll.scrollTop = scroll.scrollHeight;
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
    hubConnection.invoke("SendMessage", chatId, userId, messageText).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("messageText").value = "";
    event.preventDefault();
});
