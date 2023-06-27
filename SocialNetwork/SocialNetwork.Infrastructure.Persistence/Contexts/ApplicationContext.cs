using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API

            #region tables

            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<Friendship>().ToTable("Friendships");
            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<User>().ToTable("Users");

            #endregion

            #region "primary keys"

            modelBuilder.Entity<Comment>().HasKey(a => a.Id);
            modelBuilder.Entity<Friendship>().HasKey(d => d.Id);
            modelBuilder.Entity<Post>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            #endregion

            #region relationships

            modelBuilder.Entity<Friendship>()
                .HasOne(fs => fs.User)
                .WithMany(u => u.Friends)
                .HasForeignKey(fs => fs.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Friendship>()
                .HasOne(fs => fs.Friend)
                .WithMany(f => f.Users) 
                .HasForeignKey(fs => fs.FriendId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region "props config"


            #endregion
        }
    }
}
