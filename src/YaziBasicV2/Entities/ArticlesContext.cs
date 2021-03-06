﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Entities
{
    public class ArticlesContext : DbContext
    {
        public ArticlesContext(DbContextOptions<ArticlesContext> options)
            : base(options)
        {

        }

        public DbSet<Verity> Verity { get; set; }

        public DbSet<Ecards> Ecards { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Author> Author { get; set; }

        public DbSet<ArticleType> ArticleType { get; set; }

        public DbSet<SocialImpression> SocialImpression { get; set; }

        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<UserAndPostInfo> UserAndPostInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAndPostInfo>().HasKey(table => new { table.UserSocialId, table.PostInfoId });
        }

    }
}
