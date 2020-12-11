using FoxyBurrow.Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Database.DbContext.Mapping
{
    class ChatMap
    {
        public ChatMap(EntityTypeBuilder<Chat> entityTypeBuilder)
        {
            entityTypeBuilder.HasMany(c => c.Messages).WithOne(m => m.Chat);
        }
    }
}
