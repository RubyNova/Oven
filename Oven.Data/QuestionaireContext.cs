using Microsoft.EntityFrameworkCore;
using Oven.Data.Models;
using System;
using System.Collections.Generic;

namespace Oven.Data
{
    public class QuestionaireContext : DbContext
    {
        public DbSet<VodConfiguration> VodConfigurations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
    }
}