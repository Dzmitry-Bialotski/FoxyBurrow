using FoxyBurrow.Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Database.DbContext.Mapping
{
    class PostMap
    {
        public PostMap(EntityTypeBuilder<Post> entityTypeBuilder)
        {
            entityTypeBuilder.HasMany(p => p.Comments).WithOne(c => c.Post);
        }
    }
}
