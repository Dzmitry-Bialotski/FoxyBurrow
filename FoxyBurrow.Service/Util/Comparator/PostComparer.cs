using FoxyBurrow.Core.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FoxyBurrow.Service.Util.Comparator
{
    public class PostComparer : IComparer<Post>
    {
        //from the newest to latest
        public int Compare([AllowNull] Post post1, [AllowNull] Post post2)
        {
            if(post1.MessageDate > post2.MessageDate)
            {
                return -1;
            }
            else if(post1.MessageDate < post2.MessageDate)
            {
                return 1;
            }
            return 0;
        }
    }
}
