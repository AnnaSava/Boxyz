using Boxyz.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data
{
    public static class TestData
    {
        public static IEnumerable<ShapeBoard> GetBoards()
        {
            var boards = new List<ShapeBoard>
            {
                new ShapeBoard
                {
                    //Id = 1,
                    Level = 0,
                    Name = "media",
                    Cultures = new List<ShapeBoardCulture>
                    {
                        new ShapeBoardCulture { Culture = "ru", BoardId = 1, Title = "Медиа" },
                        new ShapeBoardCulture { Culture = "en", BoardId = 1, Title = "Media" }
                    },
                },
                new ShapeBoard
                {
                    //Id = 1,
                    Level = 0,
                    Name = "collections",
                    Cultures = new List<ShapeBoardCulture>
                    {
                        new ShapeBoardCulture { Culture = "ru", BoardId = 2, Title = "Коллекции" },
                        new ShapeBoardCulture { Culture = "en", BoardId = 2, Title = "Collections" }
                    },
                }
            };

            return boards;
        }

        public static IEnumerable<Shape> GetShapes()
        {
            var shapes = new List<Shape>
            {
                new Shape
                {
                    //Id = 1,
                    BoardId = 1,
                    ConstName = "books",
                    LastUpdated = DateTime.Now,
                    Versions = new List<ShapeVersion>
                    {
                        new ShapeVersion
                        {
                            //Id = 1,
                            ShapeId = 1,
                            IsApproved = false,
                            Created = DateTime.Now,
                            Cultures = new List<ShapeVersionCulture>
                            {
                                new ShapeVersionCulture { Culture = "ru", ShapeVersionId = 1, Title = "Книги" },
                                new ShapeVersionCulture { Culture = "en", ShapeVersionId = 1, Title = "Books" },
                            },
                            Sides = new List<ShapeSide>
                            {
                                new ShapeSide
                                {
                                    //Id = 1,
                                    ConstName = "authorName",
                                    DataType = "nativeText",
                                    Cultures = new List<ShapeSideCulture>
                                    {
                                        new ShapeSideCulture { Culture = "ru", ShapeSideId = 1, Title = "Имя автора" },
                                        new ShapeSideCulture { Culture = "en", ShapeSideId = 1, Title = "Author's name" }
                                    }
                                },
                                new ShapeSide
                                {
                                    //Id = 2,
                                    ConstName = "bookName",
                                    DataType = "nativeText",
                                    Cultures = new List<ShapeSideCulture>
                                    {
                                        new ShapeSideCulture { Culture = "ru", ShapeSideId = 2, Title = "Название книги" },
                                        new ShapeSideCulture { Culture = "en", ShapeSideId = 2, Title = "Title of the book" }
                                    }
                                },
                                new ShapeSide
                                {
                                    //Id = 3,
                                    ConstName = "year",
                                    DataType = "year",
                                    Cultures = new List<ShapeSideCulture>
                                    {
                                        new ShapeSideCulture { Culture = "ru", ShapeSideId = 3, Title = "Год издания" },
                                        new ShapeSideCulture { Culture = "en", ShapeSideId = 3, Title = "The year of publishing" }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            return shapes;
        }

        public static IEnumerable<Box> GetBoxes()
        { 
            var boxes = new List<Box>
            {
                new Box
                {
                    //Id = 1,
                    ShapeId = 1,
                    Versions = new List<BoxVersion>
                    {
                        new BoxVersion
                        {
                            //Id = 1,
                            BoxId = 1,
                            ShapeVersionId = 1,     
                            Created = DateTime.Now,
                            IsApproved = false,
                            Sides = new List<BoxSide>
                            {
                                new BoxSide
                                {
                                    //Id = 1,
                                    BoxVersionId = 1,
                                    ShapeSideId = 1,
                                    UniversalValue = "Анна Ахматова",
                                    Cultures = new List<BoxSideCulture>
                                    {
                                        new BoxSideCulture { Culture = "ru", BoxSideId = 1, Value = "Анна Ахматова" },
                                        new BoxSideCulture { Culture = "en", BoxSideId = 1, Value = "Anna Akhmatova" }
                                    }
                                },
                                new BoxSide
                                {
                                    //Id = 2,
                                    BoxVersionId = 1,
                                    ShapeSideId = 2,
                                    UniversalValue = "Вечер",
                                    Cultures = new List<BoxSideCulture>
                                    {
                                        new BoxSideCulture { Culture = "ru", BoxSideId = 2, Value = "Вечер" },
                                        new BoxSideCulture { Culture = "en", BoxSideId = 2, Value = "Vecher (Evening)" }
                                    }
                                },
                                new BoxSide
                                {
                                    //Id = 3,
                                    BoxVersionId = 1,
                                    ShapeSideId = 3,
                                    UniversalValue = "1912"
                                },
                            }
                        }
                    }
                }
            };
            return boxes;
        }
    }
}
