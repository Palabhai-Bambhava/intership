﻿using AspNetCoreCrudDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreCrudDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
