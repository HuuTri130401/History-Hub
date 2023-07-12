using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Models
{
    public partial class HistoryHubContext : DbContext
    {
        public HistoryHubContext()
        {
        }

        public HistoryHubContext(DbContextOptions<HistoryHubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Figure> Figures { get; set; } = null!;
        public virtual DbSet<Period> Periods { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<QuizAttempt> QuizAttempts { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Timeline> Timelines { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //                optionsBuilder.UseSqlServer("Server=PhiLong0103;Uid=sa;Pwd=123456;Database= HistoryHub;TrustServerCertificate=True");
            //            }
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            return configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.Property(e => e.FavoriteId).HasColumnName("Favorite_Id");

                entity.Property(e => e.QuizId).HasColumnName("Quiz_Id");

                entity.Property(e => e.TimelineId).HasColumnName("Timeline_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorites__Quiz___4E88ABD4");

                entity.HasOne(d => d.Timeline)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.TimelineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorites__Timel__4D94879B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorites__User___4CA06362");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.FeedbackId).HasColumnName("Feedback_Id");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.QuizId).HasColumnName("Quiz_Id");

                entity.Property(e => e.TimelineId).HasColumnName("Timeline_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedbacks__Quiz___49C3F6B7");

                entity.HasOne(d => d.Timeline)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.TimelineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedbacks__Timel__48CFD27E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedbacks__User___47DBAE45");
            });

            modelBuilder.Entity<Figure>(entity =>
            {
                entity.Property(e => e.FigureId).HasColumnName("Figure_Id");

                entity.Property(e => e.CreateBy).HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FigureName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Figure_Name");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PeriodId).HasColumnName("Period_Id");

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.Figures)
                    .HasForeignKey(d => d.CreateBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Figures__Create___59FA5E80");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.Figures)
                    .HasForeignKey(d => d.PeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Figures__Period___59063A47");
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.Property(e => e.PeriodId).HasColumnName("Period_Id");

                entity.Property(e => e.CreateBy).HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PeriodName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Period_Name");

                entity.Property(e => e.TimelineId).HasColumnName("Timeline_Id");

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.Periods)
                    .HasForeignKey(d => d.CreateBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Periods__Create___5629CD9C");

                entity.HasOne(d => d.Timeline)
                    .WithMany(p => p.Periods)
                    .HasForeignKey(d => d.TimelineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Periods__Timelin__5535A963");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId).HasColumnName("Question_Id");

                entity.Property(e => e.CorrectAnswer)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Correct_Answer");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.FalseAnswer1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("False_Answer1");

                entity.Property(e => e.FalseAnswer2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("False_Answer2");

                entity.Property(e => e.FalseAnswer3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("False_Answer3");

                entity.Property(e => e.NumberOfQuestion).HasColumnName("Number_of_Question");

                entity.Property(e => e.Question1)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Question");

                entity.Property(e => e.QuizId).HasColumnName("Quiz_Id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Questions__Creat__5165187F");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Questions__Quiz___52593CB8");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.Property(e => e.QuizId).HasColumnName("Quiz_Id");

                entity.Property(e => e.CompletionTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Completion_Time");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MaxScore).HasColumnName("Max_Score");

                entity.Property(e => e.NumberOfFavorite).HasColumnName("Number_of_Favorite");

                entity.Property(e => e.NumberOfFeedback).HasColumnName("Number_of_Feedback");

                entity.Property(e => e.NumberOfQuestion).HasColumnName("Number_of_Question");

                entity.Property(e => e.TimelineId).HasColumnName("Timeline_Id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Quizzes__Created__403A8C7D");

                entity.HasOne(d => d.Timeline)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.TimelineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Quizzes__Timelin__412EB0B6");
            });

            modelBuilder.Entity<QuizAttempt>(entity =>
            {
                entity.HasKey(e => e.AttemptId)
                    .HasName("PK__QuizAtte__BBC48BD2FBBBAF21");

                entity.Property(e => e.AttemptId).HasColumnName("Attempt_Id");

                entity.Property(e => e.CompletionTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Completion_Time");

                entity.Property(e => e.QuizId).HasColumnName("Quiz_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.QuizAttempts)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__QuizAttem__Quiz___44FF419A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.QuizAttempts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__QuizAttem__User___440B1D61");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Permissions)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Role_Name");
            });

            modelBuilder.Entity<Timeline>(entity =>
            {
                entity.Property(e => e.TimelineId).HasColumnName("Timeline_Id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfFavorite).HasColumnName("Number_of_Favorite");

                entity.Property(e => e.Title)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Timelines)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Timelines__Creat__3D5E1FD2");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534E68A4BD6")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__Role_Id__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
