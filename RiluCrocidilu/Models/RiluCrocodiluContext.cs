using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RiluCrocidilu.Models
{
    public partial class RiluCrocodiluContext : DbContext
    {
        public RiluCrocodiluContext()
        {
        }

        public RiluCrocodiluContext(DbContextOptions<RiluCrocodiluContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<ChatMessage> ChatMessage { get; set; }
        public virtual DbSet<ChatRoom> ChatRoom { get; set; }
        public virtual DbSet<Homework> Homework { get; set; }
        public virtual DbSet<Lesson> Lesson { get; set; }
        public virtual DbSet<LoggedInUsers> LoggedInUsers { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<ModuleUser> ModuleUser { get; set; }
        public virtual DbSet<Presentation> Presentation { get; set; }
        public virtual DbSet<PrivateMessage> PrivateMessage { get; set; }
        public virtual DbSet<Resources> Resources { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=CR002887PC\\SQLEXPRESS;Database=RiluCrocodilu;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("FK__Attendanc__Lesso__3C34F16F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Attendanc__UserI__3D2915A8");
            });

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId);

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.ChatMessage)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__ChatMessa__RoomI__44CA3770");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.ChatMessageToUser)
                    .HasForeignKey(d => d.ToUserId)
                    .HasConstraintName("FK__ChatMessa__ToUse__46B27FE2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChatMessageUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ChatMessa__UserI__45BE5BA9");
            });

            modelBuilder.Entity<ChatRoom>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasIndex(e => e.LessonId)
                    .HasName("UQ__Homework__B084ACD17BD7DE7B")
                    .IsUnique();

                entity.Property(e => e.HomeworkId).HasColumnName("HomeworkID");

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.HasOne(d => d.Lesson)
                    .WithOne(p => p.Homework)
                    .HasForeignKey<Homework>(d => d.LessonId)
                    .HasConstraintName("FK__Homework__Lesson__02084FDA");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Property(e => e.LessonId).HasColumnName("LessonID");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Lesson)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__Lesson__ModuleId__6EF57B66");
            });

            modelBuilder.Entity<LoggedInUsers>(entity =>
            {
                entity.HasKey(e => e.LoggedInUserId);

                entity.Property(e => e.LoggedInUserId).HasColumnName("LoggedInUserID");

                entity.Property(e => e.ConnectionId).HasMaxLength(256);

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.LoggedInUsers)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__LoggedInU__RoomI__4B7734FF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LoggedInUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__LoggedInU__UserI__4A8310C6");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(e => e.Details).IsUnicode(false);
            });

            modelBuilder.Entity<ModuleUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.AspNetUserId)
                    .HasName("UQ__ModuleUs__F42021A66DA1CB54")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ConnectionId).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.AspNetUser)
                    .WithOne(p => p.ModuleUser)
                    .HasForeignKey<ModuleUser>(d => d.AspNetUserId)
                    .HasConstraintName("FK__ModuleUse__AspNe__17036CC0");
            });

            modelBuilder.Entity<Presentation>(entity =>
            {
                entity.HasIndex(e => e.LessonId)
                    .HasName("UQ__Presenta__B084ACD16E7F72B2")
                    .IsUnique();

                entity.Property(e => e.PresentationId).HasColumnName("PresentationID");

                entity.Property(e => e.PresentationExtension)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Lesson)
                    .WithOne(p => p.Presentation)
                    .HasForeignKey<Presentation>(d => d.LessonId)
                    .HasConstraintName("FK__Presentat__Lesso__7E37BEF6");
            });

            modelBuilder.Entity<PrivateMessage>(entity =>
            {
                entity.Property(e => e.PrivateMessageId).HasColumnName("PrivateMessageID");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.PrivateMessageToUser)
                    .HasForeignKey(d => d.ToUserId)
                    .HasConstraintName("FK__PrivateMe__ToUse__40F9A68C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PrivateMessageUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PrivateMe__UserI__40058253");
            });

            modelBuilder.Entity<Resources>(entity =>
            {
                entity.HasKey(e => e.ResourceId);

                entity.Property(e => e.ResourceId).HasColumnName("ResourceID");

                entity.Property(e => e.ResourceData).IsUnicode(false);

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("FK__Resources__Lesso__04E4BC85");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasIndex(e => e.ModuleId)
                    .HasName("UQ__Schedule__2B7477A628580596")
                    .IsUnique();

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.ModuleDay).HasColumnType("date");

                entity.Property(e => e.ModuleLocation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Module)
                    .WithOne(p => p.Schedule)
                    .HasForeignKey<Schedule>(d => d.ModuleId)
                    .HasConstraintName("FK__Schedule__Module__7A672E12");
            });
        }
    }
}
