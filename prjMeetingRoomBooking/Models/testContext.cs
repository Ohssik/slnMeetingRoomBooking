using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace prjMeetingRoomBooking.Models
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TMeeingBooking> TMeeingBookings { get; set; } = null!;
        public virtual DbSet<TMeetingRoom> TMeetingRooms { get; set; } = null!;
        public virtual DbSet<TUser> TUsers { get; set; } = null!;
        public virtual DbSet<ViewManager2Room> ViewManager2Rooms { get; set; } = null!;
        public virtual DbSet<ViewTmeeingBooking> ViewTmeeingBookings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=test;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TMeeingBooking>(entity =>
            {
                entity.ToTable("tMeeingBooking");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookingUserId)
                    .HasMaxLength(50)
                    .HasColumnName("BookingUserID");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(50)
                    .HasColumnName("RoomID");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Subject).HasMaxLength(50);
            });

            modelBuilder.Entity<TMeetingRoom>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.ToTable("tMeetingRoom");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(50)
                    .HasColumnName("RoomID");

                entity.Property(e => e.ManagerId)
                    .HasMaxLength(50)
                    .HasColumnName("ManagerID");

                entity.Property(e => e.RoomName).HasMaxLength(50);

                entity.Property(e => e.Size).HasMaxLength(50);
            });

            modelBuilder.Entity<TUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tUser");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.UserPwd).HasMaxLength(50);
            });

            modelBuilder.Entity<ViewManager2Room>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Manager2Room");

                entity.Property(e => e.ManagerId)
                    .HasMaxLength(50)
                    .HasColumnName("ManagerID");

                entity.Property(e => e.ManagerName)
                    .HasMaxLength(50)
                    .HasColumnName("managerName");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(50)
                    .HasColumnName("RoomID");

                entity.Property(e => e.RoomName).HasMaxLength(50);

                entity.Property(e => e.Size).HasMaxLength(52);
            });

            modelBuilder.Entity<ViewTmeeingBooking>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewTMeeingBooking");

                entity.Property(e => e.BookingUserId)
                    .HasMaxLength(50)
                    .HasColumnName("BookingUserID");

                entity.Property(e => e.EndDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(50)
                    .HasColumnName("RoomID");

                entity.Property(e => e.StartDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.Subject).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
