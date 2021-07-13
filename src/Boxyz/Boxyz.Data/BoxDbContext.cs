using Boxyz.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Boxyz.Data
{
    public class BoxDbContext : DbContext
    {
        public BoxDbContext(DbContextOptions<BoxDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<BoxShapeBoard> BoxShapeBoards { get; set; }

        public DbSet<BoxShapeBoardCulture> BoxShapeBoardCultures { get; set; }

        public DbSet<BoxShape> BoxShapes { get; set; }

        public DbSet<BoxShapeVersion> BoxShapeVersions { get; set; }

        public DbSet<BoxShapeVersionCulture> BoxShapeVersionCultures { get; set; }

        public DbSet<BoxShapeSide> BoxShapeSides { get; set; }

        public DbSet<BoxShapeSideCulture> BoxShapeSideCultures { get; set; }

        public DbSet<Box> Boxes { get; set; }

        public DbSet<BoxVersion> BoxVersions { get; set; }

        public DbSet<BoxSide> BoxSides { get; set; }

        public DbSet<BoxSideCulture> BoxSideCultures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BoxShapeBoard>(b =>
            {
                b.HasMany(m => m.ChildBoards)
                    .WithOne(m => m.ParentBoard)
                    .HasForeignKey(m => m.ParentBoardId);

                b.HasMany(m => m.Cultures)
                    .WithOne(m => m.Board)
                    .HasForeignKey(m => m.BoardId)
                    .IsRequired();

                b.HasMany<BoxShape>()
                    .WithOne(m => m.Board)
                    .HasForeignKey(m => m.BoardId)
                    .IsRequired();
            });

            builder.Entity<BoxShape>(b =>
            {
                b.HasMany(m => m.Versions)
                    .WithOne(m => m.Shape)
                    .HasForeignKey(m => m.ShapeId)
                    .IsRequired();

                b.HasMany<Box>()
                    .WithOne(m => m.Shape)
                    .HasForeignKey(m => m.ShapeId)
                    .IsRequired();
            });

            builder.Entity<BoxShapeVersion>(b =>
            {
                b.HasMany(m => m.Cultures)
                    .WithOne(m => m.ShapeVersion)
                    .HasForeignKey(m => m.ShapeVersionId)
                    .IsRequired();

                b.HasMany(m => m.Sides)
                    .WithOne(m => m.ShapeVersion)
                    .HasForeignKey(m => m.ShapeVersionId)
                    .IsRequired();

                b.HasMany<BoxVersion>()
                    .WithOne(m => m.ShapeVersion)
                    .HasForeignKey(m => m.ShapeVersionId)
                    .IsRequired();
            });

            builder.Entity<BoxShapeSide>(b =>
            {
                b.HasMany(m => m.Cultures)
                    .WithOne(m => m.ShapeSide)
                    .HasForeignKey(m => m.ShapeSideId)
                    .IsRequired();

                b.HasMany<BoxSide>()
                    .WithOne(m => m.ShapeSide)
                    .HasForeignKey(m => m.ShapeSideId)
                    .IsRequired();
            });

            builder.Entity<Box>(b =>
            {
                b.HasMany(m => m.Versions)
                    .WithOne(m => m.Box)
                    .HasForeignKey(m => m.BoxId)
                    .IsRequired();
            });

            builder.Entity<BoxVersion>(b =>
            {
                b.HasMany(m => m.Sides)
                    .WithOne(m => m.BoxVersion)
                    .HasForeignKey(m => m.BoxVersionId)
                    .IsRequired();
            });

            builder.Entity<BoxSide>(b =>
            {
                b.HasMany(m => m.Cultures)
                    .WithOne(m => m.BoxSide)
                    .HasForeignKey(m => m.BoxSideId)
                    .IsRequired();
            });

            builder.Entity<BoxShapeBoardCulture>()
                .HasKey(m => new { m.Culture, m.BoardId });

            builder.Entity<BoxShapeVersionCulture>()
                .HasKey(m => new { m.Culture, m.ShapeVersionId });

            builder.Entity<BoxShapeSideCulture>()
                .HasKey(m => new { m.Culture, m.ShapeSideId });

            builder.Entity<BoxSideCulture>()
                .HasKey(m => new { m.Culture, m.BoxSideId });
        }
    }
}
