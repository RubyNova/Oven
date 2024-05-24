using System;
using Microsoft.EntityFrameworkCore;
using Oven.Data.Models;

namespace Oven.Data
{
    public class VodConfigurationContext : DbContext
    {
        public DbSet<VodConfigurationModel> VodConfigurations { get; set; }

        // "Host=localhost;Database=ovenbot;Username=Oven;Password=Testing123"
        private readonly string? _dbPath = Environment.GetEnvironmentVariable("PostgresDB");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_dbPath)
                .UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasPostgresEnum<AnswerKind>();
        }
    }
}