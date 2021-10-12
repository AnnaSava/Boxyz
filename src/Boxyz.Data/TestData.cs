using Boxyz.Proto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boxyz.Proto.Data
{
    public static class TestData
    {
        public static IEnumerable<ShapeBoard> GetBoards()
        {
            var boardsArr = new List<object[]>
            {
                new object[] { "media", "Медиа", "Media" },
                new object[] { "collections", "Коллекции", "Collections" },
                new object[] { "travel", "Путешествия", "Travel" },
                new object[] { "purchases", "Покупки", "Purchases" },
                new object[] { "figures", "Фигуры", "Figures" },
            };

            var boards = new List<ShapeBoard>();

            foreach (var arrLine in boardsArr)
            {
                boards.Add(new ShapeBoard
                {
                    Level = 0,
                    Name = arrLine[0].ToString(),
                    Cultures = new List<ShapeBoardCulture>
                    {
                        new ShapeBoardCulture { Culture = "ru", Title = arrLine[1].ToString() },
                        new ShapeBoardCulture { Culture = "en", Title = arrLine[2].ToString() }
                    },
                });
            }

            return boards;
        }

        public static IEnumerable<Shape> GetShapes(IEnumerable<ShapeBoard> boards)
        {
            var shapesArr = new List<object[]>
            {
                new object[] { "media", "books", "Книги", "Books",
                    new List<object[]>
                    {
                        new object[] { "authorName", "text", "Имя автора", "Author's name" },
                        new object[] { "bookName", "text", "Название книги", "Title of the book" },
                        new object[] { "year", "year", "Год издания", "The year of publishing" }
                    }
                },
                new object[] { "media", "movies", "Фильмы", "Movies",
                    new List<object[]>
                    {
                        new object[] { "movieName", "text", "Название фильма", "Title of the movie" },
                        new object[] { "year", "year", "Год выпуска", "The year of issue" }
                    }
                },
                new object[] { "collections", "dolls", "Куклы", "Dolls",
                    new List<object[]>
                    {
                        new object[] { "dollName", "text", "Имя куклы", "Doll's name" },
                        new object[] { "size", "integer", "Размер", "Size" },
                        new object[] { "price", "money", "Стоимость при покупке", "Cost upon purchase" }
                    }
                },
                new object[] { "travel", "hotels", "Гостиницы", "Hotels",
                    new List<object[]>
                    {
                        new object[] { "name", "text", "Название", "Name" },
                        new object[] { "whatIsNice", "text", "Чем понравилась", "What did I like" }
                    }
                },
                new object[] { "travel", "cities", "Города", "Cities",
                    new List<object[]>
                    {
                        new object[] { "name", "text", "Название", "Name" },
                        new object[] { "whatToSee", "text", "Что посмотреть", "What to see" }
                    }
                },
                new object[] { "purchases", "fruit", "Фрукты", "Fruit",
                    new List<object[]>
                    {
                        new object[] { "name", "text", "Название", "Name" },
                        new object[] { "weight", "float", "Вес", "Weight" }
                    }
                },
                new object[] { "figures", "triangles", "Треугольники", "Triangles",
                    new List<object[]>
                    {
                        new object[] { "name", "text", "Название", "Name" },
                        new object[] { "a", "point", "A", "A" },
                        new object[] { "b", "point", "B", "B" },
                        new object[] { "c", "point", "C", "C" },
                    }
                },
            };

            var shapes = new List<Shape>();

            foreach (var shapeLine in shapesArr)
            {
                var shape = new Shape
                {
                    BoardId = boards.First(b => b.Name == shapeLine[0].ToString()).Id,
                    ConstName = shapeLine[1].ToString(),
                    LastUpdated = DateTime.Now,
                    Versions = new List<ShapeVersion>
                    {
                        new ShapeVersion
                        {
                            Created = DateTime.Now,
                            Cultures = new List<ShapeVersionCulture>
                            {
                                new ShapeVersionCulture { Culture = "ru", Title = shapeLine[2].ToString() },
                                new ShapeVersionCulture { Culture = "en", Title = shapeLine[3].ToString() },
                            },
                            Sides = new List<ShapeSide> { }
                        }
                    }
                };

                foreach (var sideLine in shapeLine[4] as List<object[]>)
                {
                    shape.Versions.First().Sides.Add(new ShapeSide
                    {
                        ConstName = sideLine[0].ToString(),
                        DataType = sideLine[1].ToString(),
                        Cultures = new List<ShapeSideCulture>
                        {
                            new ShapeSideCulture { Culture = "ru", Title = sideLine[2].ToString() },
                            new ShapeSideCulture { Culture = "en", Title = sideLine[3].ToString() }
                        }
                    });
                }

                shapes.Add(shape);
            }

            return shapes;
        }

        public static IEnumerable<Box> GetBoxes(IEnumerable<Shape> shapes)
        {
            var boxesArr = new List<object[]>
            {
                new object[] { "books",
                    new List<object[]>
                    {
                        new object[] { "authorName", "Анна Ахматова", "Анна Ахматова", "Anna Akhmatova" },
                        new object[] { "bookName", "Вечер", "Вечер", "Vecher (Evening)" },
                        new object[] { "year", "1912" }
                    }
                },
                new object[] { "dolls",
                    new List<object[]>
                    {
                        new object[] { "dollName", "Barbie", "Барби", "Barbie" },
                        new object[] { "size", 29, "cm" },
                        new object[] { "price", 9.99m, "$" }
                    }
                },
                new object[] { "dolls",
                    new List<object[]>
                    {
                        new object[] { "dollName", "Sindy", "Синди", "Sindy" },
                        new object[] { "size", 29, "cm"  },
                        new object[] { "price", 10.99m, "$" }
                    }
                },
                new object[] { "dolls",
                    new List<object[]>
                    {
                        new object[] { "dollName", "Susy", "Сюзи", "Susy" },
                        new object[] { "size", 29, "cm"  },
                        new object[] { "price", 5.99m, "$" }
                    }
                },
                new object[] { "cities",
                    new List<object[]>
                    {
                        new object[] { "name", "Москва", "Москва", "Moscow" },
                        new object[] { "whatToSee", "Красная площадь", "Красная площадь", "Red Square" },
                    }
                },
                new object[] { "cities",
                    new List<object[]>
                    {
                        new object[] { "name", "London", "Лондон", "London" },
                        new object[] { "whatToSee", "Buckingham Palace", "Букингемский дворец", "Buckingham Palace" },
                    }
                },
                new object[] { "fruit",
                    new List<object[]>
                    {
                        new object[] { "name", "Яблоки", "Яблоки", "Apples" },
                        new object[] { "weight", 2.0, "kg" },
                    }
                },
                new object[] { "triangles",
                    new List<object[]>
                    {
                        new object[] { "name", "Первый", "Первый", "First" },
                        new object[] { "a", 0.0, 0.0, 0.0 },
                        new object[] { "b", 0.0, 2.0, 0.0 },
                        new object[] { "c", 1.0, 0.0, 0.0 },
                    }
                }
            };

            var boxes = new List<Box>();

            foreach (var boxLine in boxesArr)
            {
                var shape = shapes.First(s => s.ConstName == boxLine[0].ToString());
                var box = new Box
                {
                    ShapeId = shape.Id,
                    Versions = new List<BoxVersion>
                    {
                        new BoxVersion
                        {
                            ShapeVersionId = shape.Versions.First().Id,
                            Created = DateTime.Now,
                            Sides = new List<BoxSide> { }
                        }
                    }
                };

                foreach (var sideLine in boxLine[1] as List<object[]>)
                {
                    var shapeSide = shape.Versions.First().Sides.First(s => s.ConstName == sideLine[0].ToString());
                    var side = new BoxSide
                    {
                        ShapeSideId = shapeSide.Id,
                        UniversalValue = sideLine[1].ToString(),
                    };

                    switch (shapeSide.DataType)
                    {
                        case "text":
                            side.Cultures = new List<BoxSideCulture>
                            {
                                new BoxSideCulture { Culture = "ru", Value = sideLine[2].ToString() },
                                new BoxSideCulture { Culture = "en", Value = sideLine[3].ToString() }
                            };
                            break;
                        case "integer":
                            side.Integer = new BoxSideInteger { Value = (int)sideLine[1], Measure = sideLine[2].ToString() };
                            break;
                        case "float":
                            side.Float = new BoxSideFloat { Value = (double)sideLine[1], Measure = sideLine[2].ToString() };
                            break;
                        case "money":
                            side.Money = new BoxSideMoney { Value = (decimal)sideLine[1], Currency = sideLine[2].ToString() };
                            break;
                        case "point":
                            side.Point = new BoxSidePoint { X = (double)sideLine[1], Y = (double)sideLine[2], Z = (double)sideLine[3] };
                            break;
                    }

                    box.Versions.First().Sides.Add(side);
                }

                boxes.Add(box);
            }

            return boxes;
        }
    }
}
