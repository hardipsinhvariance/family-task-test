﻿using Domain.DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class FamilyTaskContext : DbContext
    {

        public FamilyTaskContext(DbContextOptions<FamilyTaskContext> options):base(options)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder
                .Entity<Member>(entity => {
                    entity.HasKey(member => member.Id);
                    entity.ToTable("Member");
                })
                .Entity<Task>(entity => {
                    entity.HasKey(task => task.Id);
                    entity.ToTable("Task");
                });
        }
    }
}