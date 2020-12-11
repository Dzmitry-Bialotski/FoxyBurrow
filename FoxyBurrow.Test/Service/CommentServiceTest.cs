using FoxyBurrow.Core.Entity;
using FoxyBurrow.Database.DbContext;
using FoxyBurrow.Database.Repositpry;
using FoxyBurrow.Service.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoxyBurrow.Test.Service
{
    public class CommentServiceTest
    {
        private readonly IConfiguration _configuration;
        private EFDbContext DbContext;
        private EFRepository<Comment> repository;
        private CommentService commentService;
        public CommentServiceTest()
        {
            //var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            _configuration = new ConfigurationBuilder().Build();

        }
        [Fact]
        public void GetCommemtTest()
        {
            var builder = new DbContextOptionsBuilder<EFDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            DbContext = new EFDbContext(builder.Options);
            repository = new EFRepository<Comment>(DbContext);
            commentService = new CommentService(repository);

            long id = 1;
            DbContext.Comments.Add(new Comment() { Id = id, Text = "Comment" });
            DbContext.SaveChanges();
            var comment = commentService.Get(id);
            Assert.NotNull(comment);
            Assert.IsType<Comment>(comment);
            Assert.Equal(id, comment.Id);
            Assert.Equal("Comment", comment.Text);
        }
    }
}
