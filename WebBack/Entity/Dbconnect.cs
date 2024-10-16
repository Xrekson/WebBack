﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata;
using WebBack.Model;

namespace WebBack.Entity
{
    public partial class Dbconnect : DbContext
    {
        public Dbconnect(DbContextOptions<Dbconnect> options) :
            base(options)
        {
        }
        public virtual DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasKey(e => new { e.Email, e.password });
            modelBuilder.Entity<Users>().HasIndex(i => i.Mobile);
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
