using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repository.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class ApplicationDbContext : IdentityDbContext<AplicationUser>
    {
        public virtual DbSet<SalaryParameters> SalaryesParameters { get; set; }

        public virtual DbSet<Month> Months { get; set; }

        public virtual DbSet<UserSalary> UsersSalaryes { get; set; }

        public virtual DbSet<AnnualSalary> AnnualSalaryes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserSalary>().HasKey(up => new { up.UserId, up.SalaryParametersId,up.MonthId });            

            builder.Entity<UserSalary>()
            .HasOne<AplicationUser>(tm => tm.User)
            .WithMany(tpp => tpp.SalarysUsers)
            .HasForeignKey(tm => tm.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserSalary>()
            .HasOne<SalaryParameters>(tm => tm.SalaryParameters)
            .WithMany(tpp => tpp.SalarysUsers)
            .HasForeignKey(tm => tm.SalaryParametersId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserSalary>()
            .HasOne<Month>(tm => tm.Month)
            .WithMany(tpp => tpp.SalarysUsers)
            .HasForeignKey(tm => tm.MonthId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SalaryParameters>().HasIndex(u => u.Year).IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
