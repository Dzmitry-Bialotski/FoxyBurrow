﻿using FoxyBurrow.Core.Entity;
using FoxyBurrow.Database.DbContext;
using FoxyBurrow.Database.Repository;
using FoxyBurrow.Service.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FoxyBurrow.Test.Service
{
    public class PostServiceTest
    {
        private readonly IConfiguration _configuration;
        private readonly EFDbContext DbContext;
        private readonly EFRepository<Post> repository;
        private readonly PostService postService;

        public PostServiceTest()
        {
            _configuration = new ConfigurationBuilder().Build();
            var builder = new DbContextOptionsBuilder<EFDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            DbContext = new EFDbContext(builder.Options);
            repository = new EFRepository<Post>(DbContext);
            postService = new PostService(repository);
        }

        [Fact]
        public void GetPostTest()
        {
            DbContext.Posts.Add(new Post() { Id = 1, Text = "Content" });
            DbContext.SaveChanges();
            var post = postService.Get(1);
            Assert.NotNull(post);
            Assert.IsType<Post>(post);
            Assert.Equal(1, post.Id);
            Assert.Equal("Content", post.Text);
        }

        [Fact]
        public void GetPostsTest()
        {
            for (int i = 1; i <= 10; i++)
            {
                DbContext.Posts.Add(new Post() { Id = i, Text = "Content" });
            }
            DbContext.SaveChanges();
            var posts = postService.GetAll();
            Assert.NotNull(posts);
            Assert.Equal(10, posts.Count());
        }

        [Fact]
        public void GetPostsByUserTest()
        {
            var user = new User { Email = "test@mail.ru", UserName = "test@maill.ru" };
            DbContext.Users.Add(user);
            for (int i = 1; i <= 10; i++)
            {
                DbContext.Posts.Add(new Post() { Id = i, Text = "Content", User = user });
            }
            DbContext.SaveChanges();
            var posts = postService.GetAll(user);
            Assert.NotNull(posts);
            Assert.Equal(10, posts.Count());
            for (int i = 0; i < posts.Count(); i++)
            {
                Assert.Equal(user, posts.ToArray()[i].User);
            }
        }

        [Fact]
        public void AddPostTest()
        {
            postService.Add(new Post() { Id = 1, Text =  "Content" });
            Assert.Equal(1, DbContext.Posts.Count(p => p.Text == "Content"));
        }

        [Fact]
        public void UpdatePostTest()
        {
            DbContext.Posts.Add(new Post() { Id = 1, Text = "Content" });
            DbContext.SaveChanges();
            Post post = DbContext.Posts.SingleOrDefault(s => s.Id == 1);
            post.Text = "Update content";
            postService.Update(post);
            Assert.Equal(1, DbContext.Posts.Count(p => p.Text == "Update content"));
            Assert.Equal("Update content", post.Text);
        }

        [Fact]
        public void RemovePostTest()
        {
            DbContext.Posts.Add(new Post() { Id = 1, Text = "Content" });
            DbContext.SaveChanges();
            postService.Remove(1);
            Assert.Equal(0, DbContext.Posts.Count());
        }
    }
}
