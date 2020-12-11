using FoxyBurrow.Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Database.DbContext.Mapping
{
    class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.HasMany(u => u.Posts).WithOne(p => p.User);
            entityTypeBuilder.HasMany(u => u.Comments).WithOne(c => c.User);
            entityTypeBuilder.HasMany(u => u.OutgoingRequests).WithOne(or => or.UserSender);
            entityTypeBuilder.HasMany(u => u.IncomingRequests).WithOne(ir => ir.UserReceiver);
            entityTypeBuilder.HasMany(u => u.Messages).WithOne(m => m.User);
            entityTypeBuilder.HasOne(u => u.UserInformation).WithOne(ui => ui.User);
        }
    }
}
