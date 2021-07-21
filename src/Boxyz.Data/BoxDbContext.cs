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

        public DbSet<ShapeBoard> ShapeBoards { get; set; }

        public DbSet<ShapeBoardCulture> ShapeBoardCultures { get; set; }

        public DbSet<Shape> Shapes { get; set; }

        public DbSet<ShapeVersion> ShapeVersions { get; set; }

        public DbSet<ShapeVersionCulture> ShapeVersionCultures { get; set; }

        public DbSet<ShapeSide> ShapeSides { get; set; }

        public DbSet<ShapeSideCulture> ShapeSideCultures { get; set; }

        public DbSet<Box> Boxes { get; set; }

        public DbSet<BoxVersion> BoxVersions { get; set; }

        public DbSet<BoxSide> BoxSides { get; set; }

        public DbSet<BoxSideCulture> BoxSideCultures { get; set; }

        public DbSet<BoxSideFloat> BoxSideFloats { get; set; }

        public DbSet<BoxSideInteger> BoxSideIntegers { get; set; }

        public DbSet<BoxSideMoney> BoxSideMoneys { get; set; }

        public DbSet<BoxSidePoint> BoxSidePoints { get; set; }

        public DbSet<BoxSideLink> BoxSideLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ShapeBoard>(b =>
            {
                b.HasMany(m => m.ChildBoards)
                    .WithOne(m => m.ParentBoard)
                    .HasForeignKey(m => m.ParentBoardId);

                b.HasMany(m => m.Cultures)
                    .WithOne(m => m.Content)
                    .HasForeignKey(m => m.ContentId)
                    .IsRequired();

                b.HasMany<Shape>()
                    .WithOne(m => m.Board)
                    .HasForeignKey(m => m.BoardId)
                    .IsRequired();
            });

            builder.Entity<Shape>(b =>
            {
                b.HasMany(m => m.Versions)
                    .WithOne(m => m.Content)
                    .HasForeignKey(m => m.ContentId)
                    .IsRequired();

                b.HasMany<Box>()
                    .WithOne(m => m.Shape)
                    .HasForeignKey(m => m.ShapeId)
                    .IsRequired();
            });

            builder.Entity<ShapeVersion>(b =>
            {
                b.HasMany(m => m.Cultures)
                    .WithOne(m => m.Content)
                    .HasForeignKey(m => m.ContentId)
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

            builder.Entity<ShapeSide>(b =>
            {
                b.HasMany(m => m.Cultures)
                    .WithOne(m => m.Content)
                    .HasForeignKey(m => m.ContentId)
                    .IsRequired();

                b.HasMany<BoxSide>()
                    .WithOne(m => m.ShapeSide)
                    .HasForeignKey(m => m.ShapeSideId)
                    .IsRequired();
            });

            builder.Entity<Box>(b =>
            {
                b.HasMany(m => m.Versions)
                    .WithOne(m => m.Content)
                    .HasForeignKey(m => m.ContentId)
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
                    .WithOne(m => m.Content)
                    .HasForeignKey(m => m.ContentId)
                    .IsRequired();

                b.HasOne(m => m.Float)
                    .WithOne(m => m.BoxSide);

                b.HasOne(m => m.Integer)
                    .WithOne(m => m.BoxSide);

                b.HasOne(m => m.Money)
                    .WithOne(m => m.BoxSide);

                b.HasOne(m => m.Point)
                    .WithOne(m => m.BoxSide);

                b.HasMany(m => m.Links)
                    .WithOne(m => m.BoxSide)
                    .HasForeignKey(m => m.BoxSideId)
                    .IsRequired();                
            });

            builder.Entity<ShapeBoardCulture>()
                .HasKey(m => new { m.Culture, m.ContentId });

            builder.Entity<ShapeVersionCulture>()
                .HasKey(m => new { m.Culture, m.ContentId });

            builder.Entity<ShapeSideCulture>()
                .HasKey(m => new { m.Culture, m.ContentId });

            builder.Entity<BoxSideCulture>()
                .HasKey(m => new { m.Culture, m.ContentId });

            builder.Entity<BoxSideFloat>().HasKey(m => m.BoxSideId);
            builder.Entity<BoxSideInteger>().HasKey(m => m.BoxSideId);
            builder.Entity<BoxSidePoint>().HasKey(m => m.BoxSideId);
            builder.Entity<BoxSideMoney>().HasKey(m => m.BoxSideId);
        }
    }
}
