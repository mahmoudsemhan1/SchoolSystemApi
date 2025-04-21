using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Domain.Models;

namespace SchoolSystem.Infrastructure.Models;

public partial class SchoolSystemDbContext : DbContext
{
    public SchoolSystemDbContext()
    {
    }

    public SchoolSystemDbContext(DbContextOptions<SchoolSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<Timetable> Timetables { get; set; }
    public virtual DbSet<TeacherClass> TeacherClasses { get; set; }
    public virtual DbSet<ClassSubjects> ClassSubjects { get; set; }
    public virtual DbSet<TeacherSubject> TeacherSubject { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-C4HF2SS;Database=SchoolSystemDB;User Id=sa;Password=P@ssw0rd;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69261C989728B2");

            entity.ToTable("Attendance");

            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Student).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Attendance_Students");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927C0B2A11FBA");

            entity.Property(e => e.ClassName).HasMaxLength(50);
        });

        modelBuilder.Entity<ClassSubjects>(entity =>
        {
            entity.HasKey(cs => new { cs.ClassID, cs.SubjectId });

            entity.HasOne(cs => cs.Class)
                  .WithMany(c => c.ClassSubjects)
                  .HasForeignKey(cs => cs.ClassID)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_ClassSubjects_Classes");

            entity.HasOne(cs => cs.subject)
                  .WithMany(s => s.ClassSubjects)
                  .HasForeignKey(cs => cs.SubjectId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_ClassSubjects_Subjects");

            entity.ToTable("ClassSubjects");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.ClassroomId).HasName("PK__Classroo__11618EAA897F6DAF");

            entity.Property(e => e.RoomNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grades__54F87A570F8CF0B9");

            entity.Property(e => e.grade)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Grade");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Grades_Students");

            entity.HasOne(d => d.Subject).WithMany(p => p.Grades)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_Grades_Subjects");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B997632E226");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_Students_Classes");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA3A8BCF38FFE");

            entity.Property(e => e.SubjectName).HasMaxLength(100);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__Teachers__EDF25964673B85A0");

            entity.Property(e => e.Name).HasMaxLength(100);

           
        });
        modelBuilder.Entity<TeacherSubject>(entity =>
        {
            entity.HasKey(ts => new { ts.TeacherId, ts.SubjectId });

            entity.HasOne(ts => ts.Teacher)
                  .WithMany(t => t.TeacherSubjects)
                  .HasForeignKey(ts => ts.TeacherId);

            entity.HasOne(ts => ts.Subject)
                  .WithMany(s => s.TeacherSubjects)
                  .HasForeignKey(ts => ts.SubjectId);

            entity.ToTable("TeacherSubject");
        });

        modelBuilder.Entity<TeacherClass>(entity =>
        {
            modelBuilder.Entity<TeacherClass>()
           .HasKey(tc => new { tc.TeacherId, tc.ClassId });

            modelBuilder.Entity<TeacherClass>()
                .HasOne(tc => tc.teacher)
                .WithMany(t => t.TeacherClasses)
                .HasForeignKey(tc => tc.TeacherId);

            modelBuilder.Entity<TeacherClass>()
                .HasOne(tc => tc.Class)
                .WithMany(c => c.TeacherClasses)
                .HasForeignKey(tc => tc.ClassId);
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");
        });

        modelBuilder.Entity<Timetable>(entity =>
        {
            entity.ToTable("Timetable");

            entity.Property(e => e.TimetableId).HasColumnName("TimetableID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.ClassroomId).HasColumnName("ClassroomID");
            entity.Property(e => e.DayOfWeek).HasMaxLength(50);
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");

            entity.HasOne(d => d.Class).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetable_Classes");

            entity.HasOne(d => d.Classroom).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.ClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetable_Classrooms");

            entity.HasOne(d => d.Subject).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetable_Subjects");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.TimeSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetable_TimeSlots");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
