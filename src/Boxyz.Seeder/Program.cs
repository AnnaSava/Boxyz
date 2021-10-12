using Boxyz.Proto.Data;
using Boxyz.Proto.Data.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Boxyz.Proto.Seeder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            await Seed();
            Console.WriteLine("Finished");
            Console.ReadKey();
        }

        static async Task Seed()
        {
            var context = GetContext();
            context.Database.Migrate();

            if (await context.ShapeBoards.AnyAsync())
                return;

            var boards = TestData.GetBoards();
            context.ShapeBoards.AddRange(boards);
            await context.SaveChangesAsync();

            var shapes = TestData.GetShapes(boards);
            context.Shapes.AddRange(shapes);
            await context.SaveChangesAsync();

            var boxes = TestData.GetBoxes(shapes);
            context.Boxes.AddRange(boxes);
            await context.SaveChangesAsync();

        }

        private static BoxDbContext GetContext()
        {
            var options = GetOptionsAction();

            var optionsBuilder = new DbContextOptionsBuilder<BoxDbContext>();
            options.Invoke(optionsBuilder);

            return new BoxDbContext(optionsBuilder.Options);
        }
        private static Action<DbContextOptionsBuilder> GetOptionsAction() => options =>
            options
                .UseNpgsql("Host=localhost;Port=5432;Database=Boxyz;User Id=postgres;Password=12345678", b => b.MigrationsAssembly("Boxyz.Migrations.PostgreSql"))
                .UseSnakeCaseNamingConvention();
    }
}
