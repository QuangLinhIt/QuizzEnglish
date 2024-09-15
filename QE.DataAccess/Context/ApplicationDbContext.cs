using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QE.Entity.Entity.Abstract.Question;
using QE.Entity.Entity.Jwt;
using QE.Entity.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using QE.Entity.Identity;
using QE.Entity.Entity.Abstract.Question.Inherit;
using QE.Entity.Enum;

namespace QE.DataAccess.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Competition> Competitions { get; set; }
        public DbSet<CompetitionQuizz> CompetitionQuizzes { get; set; }
        public DbSet<Quizz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizzScore> QuizzScores { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set; }
        public DbSet<VocabularyTopic> VocabularyTopics { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configure Table Per Hierarchy (TPH) : Question
            modelBuilder.Entity<Question>()
               .HasDiscriminator<QuestionType>("QuestionType") // Discriminator column name in the table
               .HasValue<FillinBlankQuestion>(QuestionType.FillinBlankQuestion)
               .HasValue<MultipleChoiceQuestion>(QuestionType.MultipleChoiceQuestion);

            //Quizz vs Question : many to many (QuestionQuizz)
            modelBuilder.Entity<Quizz>()
                .HasMany(q => q.Questions)
                .WithMany();

            //Competition vs Quizz: many to many (CompetitionQuizz)
            modelBuilder.Entity<CompetitionQuizz>()
                .HasKey(cq => new { cq.CompetitionId, cq.QuizzId });
            modelBuilder.Entity<CompetitionQuizz>()
                .HasOne(c => c.Competition)
                .WithMany(cq => cq.CompetitionQuizzes)
                .HasForeignKey(cq => cq.CompetitionId);
            modelBuilder.Entity<CompetitionQuizz>()
                .HasOne(q => q.Quizz)
                .WithMany()
                .HasForeignKey(cq => cq.QuizzId);

            //Quizz vs QuizzScore: one to many
            modelBuilder.Entity<QuizzScore>()
                .HasOne(q => q.Quizz)
                .WithMany(qs => qs.QuizzScores)
                .HasForeignKey(qs => qs.QuizzId);

            //Topic vs Vocabulary : many to many relationship
            modelBuilder.Entity<VocabularyTopic>()
                .HasKey(vt => new { vt.VocabularyId, vt.TopicId });
            modelBuilder.Entity<VocabularyTopic>()
                .HasOne(v => v.Vocabulary)
                .WithMany(vt => vt.VocabularyTopics)
                .HasForeignKey(vt => vt.VocabularyId)
                .IsRequired();
            modelBuilder.Entity<VocabularyTopic>()
                .HasOne(t => t.Topic)
                .WithMany(vt => vt.VocabularyTopics)
                .HasForeignKey(vt => vt.TopicId)
                .IsRequired();

            //AppUser vs RefreshToken: one to many relationship
            modelBuilder.Entity<AppUser>()
                .HasMany(r => r.RefreshTokens)
                .WithOne()
                .IsRequired();

            //Quizz vs AppUser: one to many
            modelBuilder.Entity<Quizz>()
                .HasOne(u => u.Creator)
                .WithMany()
                .HasForeignKey(q => q.CreatorId);

            //Competition vs AppUser: One to Many
            modelBuilder.Entity<Competition>()
                .HasOne(u => u.Player1)
                .WithMany()
                .HasForeignKey(c => c.Player1Id);
            modelBuilder.Entity<Competition>()
                .HasOne(u => u.Player2)
                .WithMany()
                .HasForeignKey(c => c.Player2Id);
        }
    }
}
