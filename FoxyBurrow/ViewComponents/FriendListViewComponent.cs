using FoxyBurrow.Core.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxyBurrow.ViewComponents
{
    public class FriendListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ICollection<User> model)
        {
            return View(model);
        }
    }
}
