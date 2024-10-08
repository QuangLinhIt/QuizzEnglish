﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QE.Entity.Entity.Abstract.Question;
using QE.Entity.Entity.Jwt;
using QE.Entity.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using QE.Entity.Identity;
using QE.Entity.Entity.Abstract.Question.Inherit;
using QE.Entity.Enum;
using Microsoft.Extensions.Options;

namespace QE.DataAccess.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public virtual DbSet<Competition> Competitions { get; set; }
        public virtual DbSet<CompetitionQuizz> CompetitionQuizzes { get; set; }
        public virtual DbSet<Quizz> Quizzes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionQuizz> QuestionQuizzes { get; set; }
        public virtual DbSet<QuizzScore> QuizzScores { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Vocabulary> Vocabularies { get; set; }
        public virtual DbSet<VocabularyTopic> VocabularyTopics { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=QuizzEnglish;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configure Table Per Hierarchy (TPH) : Question
            modelBuilder.Entity<Question>()
               .HasDiscriminator<QuestionType>("QuestionType") // Discriminator column name in the table
               .HasValue<FillinBlankQuestion>(QuestionType.FillinBlankQuestion)
               .HasValue<MultipleChoiceQuestion>(QuestionType.MultipleChoiceQuestion);

            //Quizz vs Question : many to many (QuestionQuizz)
            modelBuilder.Entity<QuestionQuizz>()
                .HasKey(qq => new { qq.QuestionId, qq.QuizzId });
            modelBuilder.Entity<QuestionQuizz>()
                .HasOne(q => q.Quizz)
                .WithMany(q => q.QuestionQuizzes)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<QuestionQuizz>()
                .HasOne(q => q.Question)
                .WithMany(q => q.QuestionQuizzes)
                .OnDelete(DeleteBehavior.Restrict);

            //Competition vs Quizz: many to many (CompetitionQuizz)
            modelBuilder.Entity<CompetitionQuizz>()
                .HasKey(cq => new { cq.CompetitionId, cq.QuizzId });

            modelBuilder.Entity<CompetitionQuizz>()
                .HasOne(c => c.Competition)
                .WithMany(cq => cq.CompetitionQuizzes)
                .HasForeignKey(cq => cq.CompetitionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompetitionQuizz>()
                .HasOne(q => q.Quizz)
                .WithMany()
                .HasForeignKey(cq => cq.QuizzId)
                .OnDelete(DeleteBehavior.Restrict);

            //Quizz vs QuizzScore: one to many
            modelBuilder.Entity<QuizzScore>()
                .HasOne(q => q.Quizz)
                .WithMany(qs => qs.QuizzScores)
                .HasForeignKey(qs => qs.QuizzId)
                .OnDelete(DeleteBehavior.Restrict);

            //Topic vs Vocabulary : many to many relationship
            modelBuilder.Entity<VocabularyTopic>()
                .HasKey(vt => new { vt.VocabularyId, vt.TopicId });
            modelBuilder.Entity<VocabularyTopic>()
                .HasOne(v => v.Vocabulary)
                .WithMany(vt => vt.VocabularyTopics)
                .HasForeignKey(vt => vt.VocabularyId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<VocabularyTopic>()
                .HasOne(t => t.Topic)
                .WithMany(vt => vt.VocabularyTopics)
                .HasForeignKey(vt => vt.TopicId)
                .OnDelete(DeleteBehavior.Restrict);

            //AppUser vs RefreshToken: one to many relationship
            modelBuilder.Entity<AppUser>()
                .HasMany(r => r.RefreshTokens)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            //Quizz vs AppUser: one to many
            modelBuilder.Entity<Quizz>()
                .HasOne(u => u.Creator)
                .WithMany()
                .HasForeignKey(q => q.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            //Competition vs AppUser: One to Many
            modelBuilder.Entity<Competition>()
                .HasOne(u => u.Player1)
                .WithMany()
                .HasForeignKey(c => c.Player1Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Competition>()
                .HasOne(u => u.Player2)
                .WithMany()
                .HasForeignKey(c => c.Player2Id)
                .OnDelete(DeleteBehavior.Restrict);
            //AppUser vs RefreshToken
            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.AppUser)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
