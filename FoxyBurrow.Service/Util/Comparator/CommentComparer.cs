using FoxyBurrow.Core.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FoxyBurrow.Service.Util.Comparator
{
    public class CommentComparer : IComparer<Comment>
    {
        //from the latest to newest
        public int Compare([AllowNull] Comment comment1, [AllowNull] Comment comment2)
        {
            if (comment1.MessageDate > comment2.MessageDate)
            {
                return 1;
            }
            else if (comment1.MessageDate < comment2.MessageDate)
            {
                return -1;
            }
            return 0;
        }
    }
}
