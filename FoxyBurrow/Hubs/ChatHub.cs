using FoxyBurrow.Core.Entity;
using FoxyBurrow.Service.Interface;
using FoxyBurrow.Service.Util.Image;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxyBurrow.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly IMessageService _messageService;
        public ChatHub( ILogger<ChatHub> logger, IChatService chatService, IUserService userService,
                    IImageService imageService, IMessageService messageService)
        {
            _logger = logger;
            _chatService = chatService;
            _userService = userService;
            _imageService = imageService;
            _messageService = messageService;
        }
        public async Task SendMessage(string chatId, string userId, string messageText)
        {
            if(string.IsNullOrEmpty(messageText))
            {
                return;
            }
            long longChatId = Convert.ToInt64(chatId);
            User user = await _userService.GetAsync(userId);
            string user_full_name = user.UserInformation.FirstName +" "+ user.UserInformation.SecondName;
            string imagePath = _imageService.getUserImagePath(user);
            //to make js load picture we have to remove "~/" from the begining of str
            imagePath = imagePath.Remove(0, 2);
             Message message = new Message()
            {
                Text = messageText,
                ChatId = longChatId,
                UserId = userId
            };
            await Clients.Caller.SendAsync("ReceiveMessage", imagePath, messageText, message.MessageDate.ToString("O"), true);
            await Clients.Others.SendAsync("ReceiveMessage", imagePath, messageText, message.MessageDate.ToString("O"), false);
            _logger.LogInformation($"user {user_full_name} send message");
            _messageService.Add(message);
        }
    }
}
