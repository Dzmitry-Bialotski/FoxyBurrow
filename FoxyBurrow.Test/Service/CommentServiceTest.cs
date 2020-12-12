using FoxyBurrow.Core.Entity;
using FoxyBurrow.Database.DbContext;
using FoxyBurrow.Database.Repositpry;
using FoxyBurrow.Service.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var builder = new DbContextOptionsBuilder<EFDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            DbContext = new EFDbContext(builder.Options);
            repository = new EFRepository<Comment>(DbContext);
            commentService = new CommentService(repository);
        }
        [Fact]
        public void GetCommemtTest()
        {
            long id = 1;
            DbContext.Comments.Add(new Comment() { Id = id, Text = "Comment" });
            DbContext.SaveChanges();
            var comment = commentService.Get(id);
            Assert.NotNull(comment);
            Assert.IsType<Comment>(comment);
            Assert.Equal(id, comment.Id);
            Assert.Equal("Comment", comment.Text);
        }
        [Fact]
        public async Task GetAllCommemtsTestAsync()
        {
            for (int i = 1; i <= 10; i++)
            {
                DbContext.Comments.Add(new Comment() { Id = i, Text = "Content" });
            }
            DbContext.SaveChanges();
            var comments = await commentService.GetAll().ToListAsync();
            Assert.NotNull(comments);
            Assert.Equal(10, comments.Count);
        }
        [Fact]
        public async Task GetCommentsByUserTestAsync()
        {
            var user = new User() { Email = "test@mail.ru", UserName = "test@mail.ru" };
            DbContext.Users.Add(user);
            for (int i = 0; i < 10; i++)
            {
                DbContext.Comments.Add(new Comment() { Text = "Content", User = user });
            }
            DbContext.SaveChanges();
            var comments = await commentService.GetAll(user).ToListAsync();
            Assert.NotNull(comments);
            Assert.Equal(10, comments.Count);
            for (int i = 0; i < comments.Count; i++)
            {
                Assert.Equal(user, comments.ToArray()[i].User);
            }
        }
        [Fact]
        public async Task GetCommentsByPostTestAsync()
        {
            int id = 1;
            Post post = new Post() { Id = id, Text = "Content" };
            DbContext.Posts.Add(post);
            for (int i = 2; i <= 11; i++)
            {
                DbContext.Comments.Add(new Comment() { Id = i, Text = "Content", Post = post });
            }
            DbContext.SaveChanges();
            var comments = await commentService.GetAll(post).ToListAsync();
            Assert.NotNull(comments);
            Assert.Equal(10, comments.Count);
            for (int i = 0; i < comments.Count; i++)
            {
                Assert.Equal(post, comments.ToArray()[i].Post);
            }
        }
        [Fact]
        public void AddCommentTests()
        {
            commentService.Add(new Comment() { Id = 1, Text = "Content" });
            Assert.Equal(1, DbContext.Comments.Count(p => p.Text == "Content"));
        }

        [Fact]
        public void UpdateCommentTests()
        {
            long id = 1;
            DbContext.Comments.Add(new Comment() { Id = id, Text = "Content" });
            DbContext.SaveChanges();
            Comment comment = DbContext.Comments.SingleOrDefault(s => s.Id == id);
            comment.Text = "Update content";
            commentService.Update(comment);
            Assert.Equal(1, DbContext.Comments.Count(p => p.Text == "Update content"));
        }

        [Fact]
        public void RemoveCommentTests()
        {
            long id = 1;
            DbContext.Comments.Add(new Comment() { Id = id, Text = "Content" });
            DbContext.SaveChanges();
            commentService.Remove(id);
            Assert.Equal(0, DbContext.Comments.Count(p => p.Text == "Content"));
        }
    }
}
