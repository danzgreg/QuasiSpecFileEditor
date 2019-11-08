using QuasiSpecFileEditor.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.DAL
{
    public class SpecFileDataContext : DbContext
    {
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<AlarmDescription> AlarmDescriptions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.Instructors).WithMany(i => i.Courses)
            //    .Map(t => t.MapLeftKey("CourseID")
            //        .MapRightKey("InstructorID")
            //        .ToTable("CourseInstructor"));
        }
    }
}