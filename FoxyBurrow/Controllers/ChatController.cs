using FoxyBurrow.Core.Entity;
using FoxyBurrow.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxyBurrow.Controllers
{
    public class ChatController : Controller
    {
        private readonly IUserService _userService;
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;
        public ChatController(IUserService userService, IChatService chatService, IMessageService messageService)
        {
            _userService = userService;
            _chatService = chatService;
            _messageService = messageService;
        }
        public async Task<IActionResult> Index(string userId)
        {
            User curUser = await _userService.GetAsync(User);
            User friend = await _userService.GetAsync(userId);
            Chat chat = _chatService.GetOrCreate(curUser, friend);
            return View(chat);
        }
        public async Task<IActionResult> List()
        {
            User user = await _userService.GetAsync(User);
            List<Chat> chatList = _chatService.GetAll(user).ToList();
            return View(chatList);      
        }
    }
}
